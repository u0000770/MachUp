using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantRepository
{
    public interface IPlantRepository : IDisposable
    {
        IEnumerable<PlantDomain.Plant> GetAllPlants();
        PlantDomain.Plant GetPlantBySku(string sku);
        bool AddPlant(PlantDomain.Plant plant);
        void RemovePlant(string sku);
        void UpdatePlant(PlantDomain.Plant plant);
        void Save();
    }
}
    