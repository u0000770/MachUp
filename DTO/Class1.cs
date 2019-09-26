using System;
using System.Runtime.Serialization;

namespace DTO
{

    [DataContract]
    public class PlantItem
    {
        [DataMember]
        string Sku { get; set; }
        [DataMember]
        string Name { get; set; }

    }

    [DataContract]
    public class PlantDetail : PlantItem
    {
        [DataMember]
        decimal FormSize { get; set; }
        [DataMember]
        decimal Price { get; set; }
    }


}
