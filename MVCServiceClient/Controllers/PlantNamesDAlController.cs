using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlantNameDAL;

namespace MVCServiceClient.Controllers
{
    public class PlantNamesDALController : Controller
    {
        private ModelPlantName db = new ModelPlantName();

        // GET: PlantNamesDAl
        public ActionResult Index()
        {
            var plants = db.PlantNames.Where(p => p.Active == true);
            return View(plants);
        }

        // GET: PlantNamesDAl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantName plantName = db.PlantNames.Find(id);
            if (plantName == null)
            {
                return HttpNotFound();
            }
            return View(plantName);
        }

        // GET: PlantNamesDAl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlantNamesDAl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlantId,Sku,Name")] PlantName plantName)
        {
            if (ModelState.IsValid)
            {
                db.PlantNames.Add(plantName);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plantName);
        }

        // GET: PlantNamesDAl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantName plantName = db.PlantNames.Find(id);
            if (plantName == null)
            {
                return HttpNotFound();
            }
            return View(plantName);
        }

        // POST: PlantNamesDAl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlantId,Sku,Name")] PlantName plantName)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plantName).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plantName);
        }

        // GET: PlantNamesDAl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlantName plantName = db.PlantNames.Find(id);
            if (plantName == null)
            {
                return HttpNotFound();
            }
            return View(plantName);
        }

        // POST: PlantNamesDAl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlantName plantName = db.PlantNames.Find(id);
            db.PlantNames.Remove(plantName);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
