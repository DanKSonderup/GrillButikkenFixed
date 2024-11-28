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
using System.Data.Entity;

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

        public static ProductProductionDTO GetProductProduction(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var productProduction = context.ProductProductions
                                               .FirstOrDefault(p => p.ProjectId == id);

                if (productProduction == null)
                    return null;

                return ProductProductionMapper.Map(productProduction);
            }
        }

        public static List<ProductProductionDTO> GetProductProductions()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var productProductions = context.ProductProductions.ToList();

                return productProductions.Select(p => ProductProductionMapper.Map(p)).ToList();
            }
        }

        public static ProductProductionDTO AddProductProduction(ProductProductionDTO productProductionDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.ProductProductions.Add(ProductProductionMapper.Map(productProductionDTO));
                context.SaveChanges();
            }
            return productProductionDTO;
        }

        public static ProductProductionDTO EditProductProduction(ProductProductionDTO productProductionDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                ProductProduction dataProduction = context.ProductProductions
                    .FirstOrDefault(p => p.ProjectId == productProductionDTO.ProjectId);

                if (dataProduction == null)
                {
                    throw new Exception("Produktion not found");
                }

                

                dataProduction.ProjectName = productProductionDTO.ProjectName;
                dataProduction.QuantityToProduce = productProductionDTO.QuantityToProduce;
                dataProduction.Deadline = productProductionDTO.Deadline;
                dataProduction.Status = productProductionDTO.Status;

                context.Entry(dataProduction).State = EntityState.Modified;

                context.SaveChanges();
            }
            return productProductionDTO;
        }

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