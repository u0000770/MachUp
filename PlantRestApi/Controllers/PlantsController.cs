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

        public IHttpActionResult Get()
        {
            IEnumerable<PlantDomain.Plant> all = repository.GetAllPlants();
            if (all.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                var dto = DTObjects.PlantItem.BuildDTO(all);
                return Ok(dto);
            }
      
        }

        // GET: api/Plants/sku
        public IHttpActionResult Get(string id)
        {
            var thisPlant = repository.GetPlantBySku(id);

            if (thisPlant == null)
            {
                return NotFound();
            }
            else
            {
                var dto = DTObjects.PlantDetail.BuildDTO(thisPlant);
                return Ok(dto);
            }

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
                return StatusCode(HttpStatusCode.BadRequest);
            }

            var thisPlant = repository.GetPlantBySku(plant.Sku);

            if (thisPlant == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
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
                return StatusCode(HttpStatusCode.InternalServerError);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Plants/sku
        public IHttpActionResult Delete(string sku)
        {

            var thisPlant = repository.GetPlantBySku(sku);
            if (thisPlant == null)
            {
                return  StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                repository.RemovePlant(sku);
                try
                {
                    repository.Save();
                }
                catch (Exception ex)
                {
                    return StatusCode(HttpStatusCode.InternalServerError);
                }

                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
