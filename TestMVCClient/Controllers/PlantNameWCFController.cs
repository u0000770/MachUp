using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVCClient.Controllers
{
    public class PlantNameWCFController : Controller
    {
        PlantNameWCF.PlantNameWCFClient client = new PlantNameWCF.PlantNameWCFClient();
        // GET: PlantNameWCF
        public ActionResult Index()
        {
            var result = client.GetData(41);
            ViewBag.Result = result;
            return View();
        }
    }
}