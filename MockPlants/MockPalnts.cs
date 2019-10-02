using System;
using System.Collections.Generic;

namespace MockPlants
{
  
    public class MockPlantEntities
    {
        public List<PlantDAL.Stock> Stocks { get; set; }

        public void SaveChanges()
        {

        }

        public void Dispose()
        {

        }

        public  MockPlantEntities()
        {

            List<PlantDAL.Stock> Stocks = new List<PlantDAL.Stock>();
            Stocks.Add(new PlantDAL.Stock
            {
                SKU = "ABALBA",
                Name = "Abies alba",
                 FormSize = "P2",
                  Price = 2.50m,
                   Active = true
            });
            Stocks.Add(new PlantDAL.Stock
            {
                SKU = "ABAMABIL",
                Name = "Abies amabilis",
                 FormSize = "L 12",
                  Price = 1.08m,
                Active = true
            });
            Stocks.Add(new PlantDAL.Stock
            {
                SKU = "ABBNANA",
                Name = "Abies balsamea 'Nana'",
                 FormSize = "big",
                 Price = 20.2m,
                Active = true
            });
            Stocks.Add(new PlantDAL.Stock
            {
                SKU = "ABBPICCO",
                Name = "Abies balsamea 'Piccolo'",
                FormSize = "tiny",
                 Price = 3.12m,
                Active = true
            });

            this.Stocks = Stocks;

        }


    }
}
