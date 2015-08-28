using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using System.Web.WebSockets;
using Microsoft.Ajax.Utilities;
using TheBraverest.Models;
using TheBraverest.Models.API;

namespace TheBraverest.Controllers
{
    public class ItemSetController : ApiController
    {
        private static string tempFileDirectory = ConfigurationManager.AppSettings["TempDirectory"];

        // GET: api/ItemSet
        /// <summary>
        /// Use to create the raw JSON text for the item set file OR the file itself for a random BraveChampion!
        /// </summary>
        /// <param name="version">
        /// The Version that the originally request had.  
        /// This should be pulled from the RecommendedDto.Version property. **Optional**
        /// </param>
        /// <param name="seed">
        /// The Seed that the original request had.  
        /// This should be pulled from the RecommendedDto.Seed property.  **Optional**
        /// </param>
        /// <param name="format">
        /// Determines the format of the response you'll receive. Valid values are "text", "file" or "zip".
        /// "text" will generate the raw JSON to be copied into the item set file.
        /// "file" will generate the actual item set file that needs to be copied into the game directory.
        /// The directory the file should be copied into is {installDirectory:Default=c:\Riot Games}\League of Legends\Config\Champions\{championKey}\Recommended\
        /// {championKey} is pulled from the RecommendedDto.ChampionKey property
        /// "zip" will generate the actual item set file and a .bat file that will automatically copy the file once extracted to the default League of Legends install directory 
        /// (C:\Riot Games) **Please note if the user didn't use the default install directory this option will not work for them**
        /// </param>
        /// <returns>Either JSON, the actual item set file or a zip file with the item set file and .bat file in it</returns>
        [ResponseType(typeof(RecommendedDto))]
        public async Task<IHttpActionResult> GetItemSet(string version = null, int? seed = null, string format = "text")
        {
            //Right now Rito's map api is throwing 404's for version 5.15.1 and 5.16.1: Current version is 5.16.1
            //When is back to working get rid of the hard coded version of 5.14.1
            RecommendedDto recommendedDto = await RecommendedDto.CreateRecommendedDto(version ?? "5.14.1", seed);

            if (recommendedDto.Success)
            {
                switch (format.ToLower())
                {
                    case "text":
                        return Ok(recommendedDto);
                        break;
                    case "file":
                        JavaScriptSerializer jss = new JavaScriptSerializer();

                        string content = jss.Serialize(recommendedDto);
                        byte[] abytContent = new byte[content.Length * sizeof(char)];
                        System.Buffer.BlockCopy(content.ToCharArray(), 0, abytContent, 0, abytContent.Length);

                        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                        response.Content = new StreamContent(new MemoryStream(abytContent));
                        response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                        response.Content.Headers.ContentDisposition.FileName =
                            string.Format("BRAVEREST_{0}_{1}_{2}.json", 
                                recommendedDto.ChampionKey,
                                recommendedDto.Version, 
                                recommendedDto.Seed);

                        return ResponseMessage(response);
                        break;
                    case "zip":
                        JavaScriptSerializer jssZip = new JavaScriptSerializer();

                        string zipContent = jssZip.Serialize(recommendedDto);

                        string moveBatContent = string.Format(
                            "echo off\n" +
                            "cls\n" +
                            "if not exist \"C:\\Riot Games\\League of Legends\\Config\\Champions\" goto NoBaseDirectory\n" +
                            "if exist \"C:\\Riot Games\\League of Legends\\Config\\Champions\\{0}\\Recommended\" goto MoveFile\n" +
                            "md \"C:\\Riot Games\\League of Legends\\Config\\Champions\\{0}\\Recommended\"\n" +
                            ":MoveFile\n" +
                            "move BRAVEREST_{0}_{1}_{2}.json \"C:\\Riot Games\\League of Legends\\Config\\Champions\\{0}\\Recommended\"\n" +
                            "echo Item set file successfully copied. You may now play your Braverest game evar!!1\n" +
                            "goto Done\n" +
                            ":NoBaseDirectory\n" +
                            "echo Could not find the default League of Legends directory.\n" +
                            "echo This means you have installed League of Legends into a directory that is not the standard location.\n" +
                            "echo Because of this you must first find the directory you installed League of Legends into.\n" +
                            "echo After that you must copy the BRAVEREST_{0}_{1}_{2}.json file directly into the relative \\League of Legends\\Config\\Champions\\{0}\\Recommended directory.\n" +
                            ":Done\n" +
                            "pause\n",
                            recommendedDto.ChampionKey,
                            recommendedDto.Version,
                            recommendedDto.Seed);

                        string deleteBatContent = string.Format(
                            "echo off\n" +
                            "cls\n" +
                            "if not exist \"C:\\Riot Games\\League of Legends\\Config\\Champions\\{0}\\Recommended\\BRAVEREST_{0}_{1}_{2}.json\" goto NoFile\n" +
                            "del \"C:\\Riot Games\\League of Legends\\Config\\Champions\\{0}\\Recommended\\BRAVEREST_{0}_{1}_{2}.json\"\n" +
                            "echo Item set successfully deleted.  When you're feeling Braverest get another pro build at www.TheBraverest.com\n" +
                            "goto Done\n" + 
                            ":NoFile\n" +
                            "echo Could not find the Item Set to delete.\n" +
                            "echo This could mean one of two things\n" +
                            "echo 1) You've already deleted the item set and are not the Braverest person you once were\n" +
                            "echo 2) You have installed League of Legends into a directory that is not the standard location.\n" +
                            "echo If #2 is the answer, you must first find the directory you installed League of Legends into.\n" +
                            "echo After that you must delete the BRAVEREST_{0}_{1}_{2}.json file in the relative \\League of Legends\\Config\\Champions\\{0}\\Recommended directory.\n" +
                            ":Done\n" +
                            "pause\n",
                            recommendedDto.ChampionKey,
                            recommendedDto.Version,
                            recommendedDto.Seed);

                        string filePrefix = string.Format("BRAVEREST_{0}_{1}_{2}",
                            recommendedDto.ChampionKey,
                            recommendedDto.Version,
                            recommendedDto.Seed);

                        //TODO: Find way to write only to memory and create zip file
                        string zipFileName = tempFileDirectory + "out\\" + filePrefix + ".zip";

                        if (!File.Exists(zipFileName))
                        {
                            //If provided temp directory from the web config doesn't exist create it and the out directory
                            //The temp directory must be directly off the root
                            if (!Directory.Exists(tempFileDirectory))
                            {
                                Directory.CreateDirectory(tempFileDirectory);
                                Directory.CreateDirectory(tempFileDirectory + "out\\");
                            }

                            DirectoryInfo diWrite = Directory.CreateDirectory(tempFileDirectory + filePrefix);
                            File.WriteAllText(string.Format("{0}\\{1}.json", diWrite.FullName, filePrefix), zipContent);
                            File.WriteAllText(string.Format("{0}\\CopyItemSet.bat", diWrite.FullName), moveBatContent);
                            File.WriteAllText(string.Format("{0}\\DeleteItemSet.bat", diWrite.FullName), deleteBatContent);
                            ZipFile.CreateFromDirectory(tempFileDirectory + filePrefix, zipFileName);
                            diWrite.Delete(true);
                        }

                        HttpResponseMessage responseZip = new HttpResponseMessage(HttpStatusCode.OK);

                        responseZip.Content = new StreamContent(new FileStream(zipFileName, FileMode.Open));

                        responseZip.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                        responseZip.Content.Headers.ContentDisposition.FileName =
                            string.Format("BRAVEREST_{0}_{1}_{2}.zip",
                                recommendedDto.ChampionKey,
                                recommendedDto.Version,
                                recommendedDto.Seed);

                        return ResponseMessage(responseZip);
                        break;
                    default:
                        return BadRequest();
                        break;
                }
            }
            else
            {
                return NotFound();
            }
        }

    }
}
