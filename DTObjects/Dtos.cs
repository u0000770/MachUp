using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DTObjects
{
    [DataContract]
    public class PlantItem
    {
        [DataMember]
        public string Sku { get; set; }
        [DataMember]
        public string Name { get; set; }


        public static IEnumerable<DTObjects.PlantItem> BuildDTO(IEnumerable<PlantDomain.Plant> All)
        {
            return All.Select(p => new DTObjects.PlantItem
            {
                Sku = p.SKU,
                Name = p.Name

            }).AsEnumerable();
        }

    }

    [DataContract]
    public class PlantDetail : PlantItem
    {
        [DataMember]
        public string FormSize { get; set; }
        [DataMember]
        public decimal Price { get; set; }

        public static PlantDetail BuildDTO(PlantDomain.Plant thisPlant)
        {
            return new DTObjects.PlantDetail
            {
                Sku = thisPlant.SKU,
                Name = thisPlant.Name,
                FormSize = thisPlant.FormSize,
                Price = thisPlant.Price
            };
        }
    }

    
}
