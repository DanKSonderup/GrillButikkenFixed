using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class ProductMapper
    {

        public static ProductDTO Map(Product product)
        {
            if (product == null) return null;
            else
                return new ProductDTO(
                    product.Id, 
                    product.Name, 
                    product.EstimatedProductionTime, 
                    product.RawMaterialNeeded, 
                    product.CreatedAt, 
                    product.UpdatedAt,
                    product.AmountInStock);
        }

        public static Product Map(ProductDTO productDTO)
        {
            if (productDTO == null) return null;
            else
                return new Product(
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.EstimatedProductionTime,
                    productDTO.RawMaterialNeeded,
                    productDTO.CreatedAt,
                    productDTO.UpdatedAt,
                    productDTO.AmountInStock);
        }

        internal static void Update(ProductDTO productDTO, Product product)
        {
            if (productDTO == null || product == null) return;
            product.Id = productDTO.Id;
            product.Name = productDTO.Name;
            product.EstimatedProductionTime = productDTO.EstimatedProductionTime;
            product.RawMaterialNeeded = new Dictionary<RawMaterial, double>(productDTO.RawMaterialNeeded); // Undgå referenceændringer
            product.CreatedAt = productDTO.CreatedAt;
            product.UpdatedAt = productDTO.UpdatedAt;
            product.AmountInStock = productDTO.AmountInStock;

        }

    }
}