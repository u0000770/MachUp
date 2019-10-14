using PlantRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace PlantRestApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlantsController : ApiController
    {
        private IPlantRepository repository;


        // DB Injection
        public PlantsController()
        {
            this.repository = new PlantRepository.PlantRepository(new PlantDAL.plantsEntities());
        }

        // Mock injection
        //public PlantsController()
        //{
        //    this.repository = new PlantRepository.PlantRepository(new MockPlants.MockPlantEntities());
        //}

        public PlantsController(IPlantRepository repository)
        {
            this.repository = repository;
        }


        // GET: api/Plants
        public IEnumerable<DTObjects.PlantItem> Get()
        {
            IEnumerable<PlantDomain.Plant> all = repository.GetAllPlants();
            return DTObjects.PlantItem.BuildDTO(all);
        }

        // GET: api/Plants/5
        public DTObjects.PlantDetail Get(string id)
        {
            var thisPlant = repository.GetPlantBySku(id);
            return DTObjects.PlantDetail.BuildDTO(thisPlant);
        }

        // POST: api/Plants

       
        public IHttpActionResult PostStock(DTObjects.PlantDetail plant)
        {
            PlantDomain.Plant stockItem = new PlantDomain.Plant();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                stockItem.SKU = plant.Sku;
                stockItem.Name = plant.Name;
                stockItem.FormSize = plant.FormSize;
                stockItem.Price = plant.Price;
                if (repository.AddPlant(stockItem))
                {
                    repository.Save();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }

            return CreatedAtRoute("DefaultApi", new { sku = stockItem.SKU }, stockItem);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock(DTObjects.PlantDetail plant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var thisPlant = repository.GetPlantBySku(plant.Sku);

            if (thisPlant == null)
            {
                return BadRequest();
            }

            thisPlant.FormSize = plant.FormSize;
            thisPlant.Name = plant.Name;
            thisPlant.Price = plant.Price;

            repository.UpdatePlant(thisPlant);

            try
            {
                repository.Save();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Plants/5
        public void Delete(int id)
        {
        }
    }
}
