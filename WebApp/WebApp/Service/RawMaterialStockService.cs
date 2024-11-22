using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;

namespace WebApp.Service
{
    public class RawMaterialStockService
    {
        public static List<RawMaterialStockDTO> GetRawMaterialStocks(RawMaterialDTO rawMaterial)
        {
            return RawMaterialStockRepository.GetRawMaterialStocks(rawMaterial);
        }

        public static RawMaterialStockDTO AddRawMaterialStock(RawMaterialStockDTO rawMaterialStock)
        {
            return RawMaterialStockRepository.AddRawMaterialStock(rawMaterialStock);
        }

        public static void RemoveRawMaterialStock(int stockId, double amount)
        {
            RawMaterialStockRepository.RemoveRawMaterialStock(stockId, amount);
        }
    }
}