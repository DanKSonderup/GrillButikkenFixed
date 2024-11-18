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

        public static List<RawMaterialStock> Map(List<RawMaterialStockDTO> stockDTOs)
        {
            return stockDTOs.Select(dto => new RawMaterialStock
            {
                Id = dto.Id,
                Amount = dto.Amount,
                ExpirationDate = dto.ExpirationDate,
                RawMaterialId = dto.RawMaterialId
            }).ToList();
        }

        public static List<RawMaterialStockDTO> Map(List<RawMaterialStock> stocks)
        {
            return stocks.Select(model => new RawMaterialStockDTO
            {
                Id = model.Id,
                Amount = model.Amount,
                ExpirationDate = model.ExpirationDate,
                RawMaterialId = model.RawMaterialId
            }).ToList();
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