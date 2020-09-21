using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCServiceClient.Controllers
{
    public class PlantNameWCFController : Controller
    {

        PlantNameWCF.PlantNameWCFClient plantNameWCFClient = new PlantNameWCF.PlantNameWCFClient();
        // GET: PlantNameWCF
        public ActionResult Index()
        {
            var result = plantNameWCFClient.GetData(42);
            ViewBag.result = result;
            return View();
        }
    }
}