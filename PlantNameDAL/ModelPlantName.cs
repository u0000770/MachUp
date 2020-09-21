namespace PlantNameDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelPlantName : DbContext
    {
        public ModelPlantName()
            : base("name=ModelPlantName")
        {
        }

        public virtual DbSet<PlantName> PlantNames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
