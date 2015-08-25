using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using TheBraverest.Models;
using TheBraverest.Models.API;

namespace TheBraverest.Controllers
{
    public class ItemSetController : ApiController
    {
        // GET: api/ItemSet
        /// <summary>
        /// Use to create the raw JSON text for the item set file OR the file itself for a random BraveChampion!
        /// </summary>
        /// <param name="format">
        /// Determines the format of the response you'll receive. Valid values are "text" and "file".
        /// "text" will generate the raw JSON to be copied into the item set file.
        /// "file" will generate the actual item set file that needs to be copied into the game directory.
        /// The Path is League of Legends\Config\Champions\{championKey}\Recommended\
        /// {championKey} is pulled from the RecommendedDto.ChampionKey property
        /// </param>
        /// <returns>Either JSON or the actual item set file</returns>
        [ResponseType(typeof(RecommendedDto))]
        public async Task<IHttpActionResult> GetItemSet(string format = "text")
        {
            //Right now Rito's map api is throwing 404's for version 5.15.1 and 5.16.1: Current version is 5.16.1
            //When is back to working get rid of the hard coded version of 5.14.1
            RecommendedDto recommendedDto = await RecommendedDto.CreateRecommendedDto("5.14.1", null);

            if (recommendedDto.Success)
            {
                return Ok(recommendedDto);
            }
            else
            {
                return NotFound();
            }
        }


        // GET: api/ItemSet/{version}/{seed}
        /// <summary>
        /// Use this to recreate an originally random ItemSet.  
        /// This is generally used to validate that someone is the Braverest person they claim to be.
        /// </summary>
        /// <param name="version">
        /// The Version that the originally request had.  
        /// This should be pulled from the RecommendedDto.Version property.
        /// </param>
        /// <param name="seed">
        /// The Seed that the original request had.  
        /// This should be pulled from the RecommendedDto.Seed property.
        /// </param>
        /// <returns>The recreated RecommendedDto based off the RecommendedDto.Version and RecommendedDto.Seed provided</returns>
        [ResponseType(typeof(RecommendedDto))]
        public async Task<IHttpActionResult> GetBraveChampion(string version, int seed)
        {
            RecommendedDto recommendedDto = await RecommendedDto.CreateRecommendedDto(version, seed);

            if (recommendedDto.Success)
            {
                return Ok(recommendedDto);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
