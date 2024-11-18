using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class ProductProductionMapper
    {
        public static ProductProductionDTO Map(ProductProduction productProduction)
        {
            if (productProduction != null)
                return new ProductProductionDTO
                {
                    ProjectId = productProduction.ProjectId,
                    ProjectName = productProduction.ProjectName,
                    Product = productProduction.Product,
                    QuantityToProduce = productProduction.QuantityToProduce,
                    TimeSpent = productProduction.TimeSpent,
                    CreatedAt = productProduction.CreatedAt,
                    Deadline = productProduction.Deadline,
                };
            else
                return null;
        }
        public static ProductProduction Map(ProductProductionDTO productProductionDTO)
        {
            if (productProductionDTO != null)
                return new ProductProduction(productProductionDTO.ProjectName, productProductionDTO.Product, productProductionDTO.QuantityToProduce, productProductionDTO.CreatedAt, productProductionDTO.Deadline, productProductionDTO.Status);
            else
                return null;
        }

        internal static void Update(ProductProductionDTO productProductionDTO, ProductProduction productProduction)
        {
            if (productProductionDTO != null)
            {
                productProduction.ProjectId = productProductionDTO.ProjectId;
                productProduction.ProjectName = productProductionDTO.ProjectName;
                productProduction.Product = productProductionDTO.Product;
                productProduction.QuantityToProduce = productProductionDTO.QuantityToProduce;
                productProduction.CreatedAt = productProductionDTO.CreatedAt;
                productProduction.Deadline = productProductionDTO.Deadline;
                productProduction.TimeSpent = productProductionDTO.TimeSpent;
                productProduction.Status = productProductionDTO.Status;
            }
            else
                productProduction = null;
        }

        private static List<ProductProductionDTO> Map(List<ProductProduction> productProductions)
        {
            List<ProductProductionDTO> retur = new List<ProductProductionDTO>();
            foreach (ProductProduction productProduction in productProductions)
            {
                retur.Add(ProductProductionMapper.Map(productProduction));
            }
            return retur;
        }
    }
}