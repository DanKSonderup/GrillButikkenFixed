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
            if (product == null)
            {
                return null;
            } else
            {
                var dto = new ProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    EstimatedProductionTime = product.EstimatedProductionTime,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt,
                    AmountInStock = product.AmountInStock
                };

                dto.Id = product.Id;

                return dto;
            }
              
        }

        public static Product Map(ProductDTO productDTO)
        {
            if (productDTO == null) return null;
            else
                return new Product(
                    productDTO.Name,
                    productDTO.EstimatedProductionTime,
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
            product.CreatedAt = productDTO.CreatedAt;
            product.UpdatedAt = productDTO.UpdatedAt;
            product.AmountInStock = productDTO.AmountInStock;

        }

    }
}