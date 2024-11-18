using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Context;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public class RawMaterialStockRepository
    {
        public static List<RawMaterialStockDTO> GetRawMaterialStocks(RawMaterialDTO rawMaterial)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var stocks = context.RawMaterialsStock.
                    Where(r => r.RawMaterialId == rawMaterial.Material_id)
                    .ToList();

                return stocks.Select(r => RawMaterialStockMapper.Map(r)).ToList();
            }
        }

        public static RawMaterialStockDTO AddRawMaterialStock(RawMaterialStockDTO stockDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var newStock = new RawMaterialStock
                {
                    Amount = stockDTO.Amount,
                    ExpirationDate = stockDTO.ExpirationDate,
                    RawMaterialId = stockDTO.RawMaterialId
                };

                context.RawMaterialsStock.Add(newStock);
                context.SaveChanges();

                return RawMaterialStockMapper.Map(newStock);
            }
        }

        public static void RemoveRawMaterialStock(int stockId, double amount)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                // Find RawMaterialStock i databasen
                var stock = context.RawMaterialsStock.SingleOrDefault(s => s.Id == stockId);
                if (stock == null)
                {
                    throw new Exception($"RawMaterialStock med ID {stockId} blev ikke fundet.");
                }

                // Justér mængden eller fjern posten, hvis mængden når 0
                if (amount >= stock.Amount)
                {
                    context.RawMaterialsStock.Remove(stock);
                }
                else
                {
                    stock.Amount -= amount;
                }

                // Gem ændringer i databasen
                context.SaveChanges();

                // Opdater den tilhørende RawMaterial
                var rawMaterial = context.RawMaterials
                    .SingleOrDefault(rm => rm.Material_id == stock.RawMaterialId);

                if (rawMaterial != null)
                {
                    var rawMaterialStock = rawMaterial.Stocks.SingleOrDefault(s => s.Id == stockId);
                    if (rawMaterialStock != null)
                    {
                        rawMaterialStock.Amount -= amount;

                        if (rawMaterialStock.Amount <= 0)
                        {
                            rawMaterial.Stocks.Remove(rawMaterialStock);
                        }
                    }
                }
            }
        }
    }
}