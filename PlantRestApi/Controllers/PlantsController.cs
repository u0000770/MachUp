using PlantRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PlantRestApi.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlantsController : ApiController
    {
        private IPlantRepository repository;

        public PlantsController()
        {
            this.repository = new PlantRepository.PlantRepository(new PlantDAL.plantsEntities());
        }




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
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Plants/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Plants/5
        public void Delete(int id)
        {
        }
    }
}
