using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.DataAccess;
using WebApp.Models;
using WebApp.DataAccess.Context;
using WebApp.DTO.Mappers;

namespace WebApp.DataAccess.Repositories
{
    public class ProductProductionRepository
    {
        public static ProductProductionDTO GetProductProduction(string projectName)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var productProduction = context.ProductProductions
                                               .FirstOrDefault(p => p.ProjectName == projectName);

                if (productProduction == null)
                    return null;

                return ProductProductionMapper.Map(productProduction);
            }
        }


        // Get all ProductProductions
        public static List<ProductProductionDTO> GetProductProductions()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var productProductions = context.ProductProductions.ToList();

                return productProductions.Select(p => ProductProductionMapper.Map(p)).ToList();
            }
        }

        // Add ProductProduction
        public static ProductProductionDTO AddProductProduction(ProductProductionDTO productProductionDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.ProductProductions.Add(ProductProductionMapper.Map(productProductionDTO));
                context.SaveChanges();
            }
            return productProductionDTO;
        }

        // Edit / Update ProductProduction
        public static ProductProductionDTO EditProductProduction(ProductProductionDTO productProductionDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                ProductProduction productProduction = context.ProductProductions
                                                             .Find(productProductionDTO.ProjectId);
                ProductProductionMapper.Update(productProductionDTO, productProduction);

                context.SaveChanges();
            }
            return productProductionDTO;
        }

        //  Update ProductProductionStatus
        public static ProductProductionDTO UpdateProductProductionStatus(Status status, ProductProductionDTO productProductionDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                ProductProduction productProduction = context.ProductProductions.Find(productProductionDTO.ProjectId);

                if (productProduction != null)
                {
                    productProduction.Status = status;
                    productProductionDTO.Status = status;

                    context.SaveChanges();
                }

                return productProductionDTO;
            }
        }


        // Delete ProductProduction
        public static void DeleteProductProduction(ProductProductionDTO productProductionDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var productProduction = context.ProductProductions
                                               .FirstOrDefault(p => p.ProjectId == productProductionDTO.ProjectId);

                if (productProduction != null)
                {
                    context.ProductProductions.Remove(productProduction);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Product production not found.");
                }
            }
        }

    }
}