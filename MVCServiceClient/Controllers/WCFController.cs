using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCServiceClient.Controllers
{
    public class WCFController : Controller
    {
        PlantWCF.PlantsClient client = new PlantWCF.PlantsClient();
        
        // GET: WCF
        public ActionResult Index()
        {
            IEnumerable<PlantWCF.PlantItem> AllPlants = client.GetAll();
            IEnumerable<ViewModels.PlantItemVM> ViewModel = ViewModels.PlantItemVM.buildModel(AllPlants);
            return View(ViewModel);
        }

        public ActionResult Details(string id)
        {
            PlantWCF.PlantDetail aPlant = client.GetBySKU(id);
            ViewModels.PlantDetailVM ViewModel = ViewModels.PlantDetailVM.buildModel(aPlant);
            return View(ViewModel);
        }
    }
}