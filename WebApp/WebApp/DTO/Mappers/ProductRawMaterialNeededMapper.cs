using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public static class ProductRawMaterialNeededMapper
    {
        public static ProductRawMaterialNeededDTO Map(ProductRawMaterialNeeded entity)
        {
            return new ProductRawMaterialNeededDTO
            {
                Id = entity.Id,
                RawMaterial = entity.RawMaterial,
                RawMaterialName = entity.RawMaterial?.Name,
                Quantity = entity.Quantity
            };
        }

        public static ProductRawMaterialNeeded Map(ProductRawMaterialNeededDTO dto)
        {
            return new ProductRawMaterialNeeded
            {
                Id = dto.Id,
                RawMaterial = dto.RawMaterial,
                Quantity = dto.Quantity
            };
        }

        public static void Update(ProductRawMaterialNeededDTO dto, ProductRawMaterialNeeded entity)
        {
            entity.Quantity = dto.Quantity;

            if (entity.RawMaterial == null)
            {
                entity.RawMaterial = dto.RawMaterial; // Update RawMaterial reference
            }
        }
    }

}