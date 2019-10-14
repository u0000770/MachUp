using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlantDAL;
using PlantRepository;

namespace PlantMVCAdmin.Controllers
{
    public class StocksController : Controller
    {
        private IPlantRepository  repository;

        /// <summary>
        /// DB Injection
        /// </summary>
        public StocksController()
        {
            this.repository = new PlantRepository.PlantRepository(new PlantDAL.plantsEntities());
        }

        /// <summary>
        /// Mock Injection
        /// </summary>
        //public StocksController()
        //{
        //    this.repository = new PlantRepository.PlantRepository(new MockPlants.MockPlantEntities());
        //}




        public StocksController(IPlantRepository repository)
        {
            this.repository = repository;
        }

        // GET: Stocks
        public ActionResult Index()
        {
            IEnumerable<PlantDomain.Plant> VM = repository.GetAllPlants();
            return View(VM);
        }

        // GET: Stocks/Details/5
        public ActionResult Details(string sku)
        {
            if (sku == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantDomain.Plant plant = repository.GetPlantBySku(sku);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stocks/Create
        // THIS  PREVENTs DUPLICATE SKU ?????
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlantDomain.Plant plant)
        {
            if (ModelState.IsValid)
            {
                if (repository.AddPlant(plant))
                {
                    repository.Save();
                }
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(string sku)
        {
         
            if (sku == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantDomain.Plant plant = repository.GetPlantBySku(sku);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlantDomain.Plant plant)
        {
            if (ModelState.IsValid)
            {
                repository.UpdatePlant(plant);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(plant);
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(string sku)
        {
            if (sku == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantDomain.Plant plant = repository.GetPlantBySku(sku);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string sku)
        {
            repository.RemovePlant(sku);
            repository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
