using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantRepository
{
    public class PlantRepository: IPlantRepository
    {
        private PlantDAL.plantsEntities context;

        public PlantRepository(PlantDAL.plantsEntities context)
        {
            this.context = context;
        }

        public IEnumerable<PlantDomain.Plant> GetAllPlants()
        {
            IEnumerable<PlantDAL.Stock> all = context.Stocks.Where(stock => stock.Active != false || stock.Active == true).OrderBy(s => s.SKU).Take(10);
            return all.Select(s => new PlantDomain.Plant
            {
                SKU = s.SKU,
                Name = s.Name,
                FormSize = s.FormSize,
                Price = s.Price
            }
            ).AsEnumerable();
        }

        public PlantDomain.Plant GetPlantBySku(string sku)
        {
            PlantDAL.Stock stockItem = context.Stocks.SingleOrDefault(s => s.SKU == sku && (s.Active == null || s.Active == true));
            if (stockItem.Active != false)
            {
                return new PlantDomain.Plant
                {
                    SKU = stockItem.SKU,
                    Name = stockItem.Name,
                    FormSize = stockItem.FormSize,
                    Price = stockItem.Price
                };
            }
            else
            {
                return null;
            }


        }


        public void AddPlant(PlantDomain.Plant plant)
        {
            PlantDAL.Stock stockItem = new PlantDAL.Stock
            {
                SKU = plant.SKU,
                Name = plant.Name,
                FormSize = plant.FormSize,
                Price = plant.Price,
                Active = true
            };
            context.Stocks.Add(stockItem);

        }


        public void RemovePlant(string sku)
        {
            PlantDAL.Stock stockItem = context.Stocks.SingleOrDefault(s => s.SKU == sku);
            stockItem.Active = false;
            context.Entry(stockItem).State = EntityState.Modified;

        }


        public void UpdatePlant(PlantDomain.Plant plant)
        {

            PlantDAL.Stock stockItem = context.Stocks.SingleOrDefault(s => s.SKU == plant.SKU);
            stockItem.Name = plant.Name;
            stockItem.Price = plant.Price;
            stockItem.FormSize = plant.FormSize;
            context.Entry(stockItem).State = EntityState.Modified;

        }


        public void Save()
        {
            context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
