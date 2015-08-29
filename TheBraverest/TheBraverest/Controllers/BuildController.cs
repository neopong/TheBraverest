using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace TheBraverest.Controllers
{
    public class BuildController : AsyncController
    {
        // GET: Build
        public async Task<ActionResult> Index(String version, int? seed)
        {
            var controller =  new BraveChampionController();

            var actionResult = await controller.GetBraveChampion(version, seed) as OkNegotiatedContentResult<TheBraverest.Models.BraveChampion>;
            var data = await actionResult.ExecuteAsync(new System.Threading.CancellationToken());
            
            return View();
        }
    }
}