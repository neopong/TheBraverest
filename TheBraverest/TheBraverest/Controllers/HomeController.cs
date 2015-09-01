using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheBraverest.Models;

namespace TheBraverest.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home/Index")]
        [Route("Home/Index/{version}/{seed:int}")]
        public ActionResult Index(String version, int? seed)
        {
            return View(new HomeIndexViewModel() {
                Version = version,
                Seed = seed
            });

        }

        [Route("Rules")]
        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
    }
}