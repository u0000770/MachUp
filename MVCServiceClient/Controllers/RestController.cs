using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCServiceClient.Controllers
{


    public class RestController : Controller
    {

        private static HttpClient PlantClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("http://localhost:56726/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            return client;
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // THIS DOES NOT PREVENT DUPLICATE SKU ?????
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MVCServiceClient.ViewModels.PlantDetailVM plant)
        {
            var client = PlantClient();
            if (ModelState.IsValid)
            {
                DTObjects.PlantDetail dto = MVCServiceClient.ViewModels.PlantDetailVM.buildModel(plant);
                HttpResponseMessage response = null;
                try
                {
                    response = client.PostAsJsonAsync("api/plants", dto).Result;
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException)
                {
                    //if (response.StatusCode == HttpStatusCode.NotFound) // 404
                    //{
                    //    return View(plant);
                    //}

                    return View(plant);

                }
                // return URI of the created resource.
               // return response.Headers.Location;
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        private static IEnumerable<DTObjects.PlantItem> GetPlants(HttpClient client)
        {
            HttpResponseMessage response = client.GetAsync("api/plants").Result;
            if (response.IsSuccessStatusCode)
            {
                 return response.Content.ReadAsAsync<IEnumerable<DTObjects.PlantItem>>().Result;
            }
            else
            {
                Debug.WriteLine("Received a bad response from the web service.");
                return null;
            }
        }


        private static DTObjects.PlantDetail GetPlantBySku(HttpClient client, string id)
        {
            string request = "api/plants/" + id;
            HttpResponseMessage response = client.GetAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<DTObjects.PlantDetail>(content);
                return result;

            }
            else
            {
                Debug.WriteLine("Received a bad response from the web service.");
                return null;
            }
        }
        // GET: Rest
        public ActionResult Index()
        {
            HttpClient client = PlantClient();
            IEnumerable<DTObjects.PlantItem> AllPlants = GetPlants(client);
            IEnumerable<MVCServiceClient.ViewModels.PlantItemVM> ViewModel = ViewModels.PlantItemVM.buildModel(AllPlants);
            return View(ViewModel);

        }

        public ActionResult Details(string id)
        {

            HttpClient client = PlantClient();
            DTObjects.PlantDetail plant = GetPlantBySku(client,id);
            MVCServiceClient.ViewModels.PlantDetailVM ViewModel = ViewModels.PlantDetailVM.buildModel(plant);
            return View(ViewModel);

        }



    }
}