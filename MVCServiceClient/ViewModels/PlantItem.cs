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
}