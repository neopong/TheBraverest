using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Microsoft.Ajax.Utilities;
using TheBraverest.Classes;
using TheBraverest.Models;
using TheBraverest.Models.API;

namespace TheBraverest.Controllers
{
    public class BraveChampionController : ApiController
    {

        // GET: api/BraveChampion
        /// <summary>
        /// Use to get a random BraveChampion for fun and profit.
        /// </summary>
        /// <returns>A BraveChampion to build an item set or visual display for</returns>
        [ResponseType(typeof(BraveChampion))]
        public async Task<IHttpActionResult> GetBraveChampion()
        {
            //Right now Rito's map api is throwing 404's for version 5.15.1 and 5.16.1: Current version is 5.16.1
            //When is back to working get rid of the hard coded version of 5.14.1
            BraveChampion braveChampion = await BraveChampion.Create("5.14.1", null);

            if (!braveChampion.Success)
            {
                return NotFound();
            }

            return Ok(braveChampion);
        }

        // GET: api/BraveChampion/{version}/{seed}
        /// <summary>
        /// Use this to recreate an originally random BraveChampion.  
        /// This is generally used to validate that someone is the Braverest person they claim to be.
        /// </summary>
        /// <param name="version">
        /// The Version that the originally request had.  
        /// This should be pulled from the BraveChampion.Version property.
        /// </param>
        /// <param name="seed">
        /// The Seed that the original request had.  
        /// This should be pulled from the BraveChampion.Seed property.
        /// </param>
        /// <returns>The recreated BraveChampion based off the BraveChampion.Version and BraveChampion.Seed provided</returns>
        [ResponseType(typeof(BraveChampion))]
        public async Task<IHttpActionResult> GetBraveChampion(string version, int seed)
        {
            BraveChampion braveChampion = await BraveChampion.Create(version, seed);

            if (!braveChampion.Success)
            {
                return NotFound();
            }

            return Ok(braveChampion);
        }

    }
}
