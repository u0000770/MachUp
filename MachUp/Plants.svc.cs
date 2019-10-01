using PlantRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MachUp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Plants" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Plants.svc or Plants.svc.cs at the Solution Explorer and start debugging.
    public class Plants : IPlants
    {
        private IPlantRepository repository;

        public Plants()
        {
            this.repository = new PlantRepository.PlantRepository(new PlantDAL.plantsEntities());
        }

        //public Plants()
        //{
        //    this.repository = new PlantRepository.PlantRepository(new MockPlants.MockPlantEntities());
        //}

        public IEnumerable<DTObjects.PlantItem> GetAll()
        {
            var AllPlants = repository.GetAllPlants();
            return AllPlants.Select(p => new DTObjects.PlantItem
            {
                 Sku = p.SKU,
                  Name = p.Name

            }).AsEnumerable();
        }

        public DTObjects.PlantDetail GetBySKU(string sku)
        {
            var thisPlant = repository.GetPlantBySku(sku);
            return DTObjects.PlantDetail.BuildDTO(thisPlant);
         

        }
    }
}
