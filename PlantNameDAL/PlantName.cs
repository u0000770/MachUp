namespace PlantNameDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PlantName
    {

        public PlantName() { }

        [Key]
        public int PlantId { get; set; }

        [Required]
        [StringLength(255)]
        public string Sku { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool? Active { get; set; }
    }
}
