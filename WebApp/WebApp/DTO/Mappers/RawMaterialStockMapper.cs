using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class RawMaterialStockMapper
    {
        public static RawMaterialStockDTO Map(RawMaterialStock rawMaterialStock)
        {
            if (rawMaterialStock != null)
                return new RawMaterialStockDTO(rawMaterialStock.RawMaterialId, rawMaterialStock.Amount, rawMaterialStock.ExpirationDate);
            else return null;
        }

        public static RawMaterialStock Map(RawMaterialStockDTO rawMaterialStock)
        {
            if(rawMaterialStock != null)
                return new RawMaterialStock(rawMaterialStock.RawMaterialId,rawMaterialStock.Amount, rawMaterialStock.ExpirationDate);
            else return null;
        }

        public static void Update(RawMaterialStockDTO dto, RawMaterialStock model)
        {
            if (dto != null)
            {
                model.Amount = dto.Amount;
                model.ExpirationDate = dto.ExpirationDate;
            }
            else
                model = null;
        }
    }
}