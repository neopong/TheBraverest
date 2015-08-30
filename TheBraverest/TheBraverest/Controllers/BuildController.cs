using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace TheBraverest.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BuildController : AsyncController
    {
        /// <summary>
        /// Rebuild your build given a version and a seed.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public async Task<ActionResult> Index(String version, int? seed)
        {
            var controller =  new BraveChampionController();
            var actionResult = await controller.GetBraveChampion(version, seed) as OkNegotiatedContentResult<TheBraverest.Models.BraveChampion>;
            return View(actionResult.Content);
        }
    }
}