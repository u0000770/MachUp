using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DTObjects;

namespace MVCServiceClient.ViewModels
{



    public class PlantItemVM
    {
        [Display(Name = "Stock Number")]
        public string sku { get; set; }
        [Display(Name = "Plant Name")]
        public string name { get; set; }


        public static IEnumerable<PlantItemVM> buildModel(IEnumerable<PlantWCF.PlantItem> all)
        {
            return all.Select(p => new PlantItemVM
            {
                sku = p.Sku,
                name = p.Name

            }).AsEnumerable();
        }

      

        public static IEnumerable<PlantItemVM> buildModel(IEnumerable<DTObjects.PlantItem> all)
        {
            return all.Select(p => new PlantItemVM
            {
                sku = p.Sku,
                name = p.Name

            }).AsEnumerable();
        }
    }



    public class PlantDetailVM : PlantItem
    {
        [Display(Name = "Form Type")]
        public string formSize { get; set; }
        [Display(Name = "Price")]
        public decimal price { get; set; }


        public static PlantDetailVM buildModel(DTObjects.PlantDetail plant)
        {
            return new PlantDetailVM
            {
                Sku = plant.Sku,
                Name = plant.Name,
                formSize = plant.FormSize,
                price = plant.Price

            };
        }

        public static PlantDetailVM buildModel(PlantWCF.PlantDetail plant)
        {
            return new PlantDetailVM
            {
                 
                sku = plant.Sku,
                name = plant.Name,
                formSize = plant.FormSize,
                price = plant.Price

            };
        }
    }
}