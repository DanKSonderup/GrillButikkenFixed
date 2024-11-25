using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApp.DataAccess.Context;
using WebApp.DTO;
using WebApp.DTO.Mappers;

namespace WebApp.DataAccess.Repositories
{
    public class ProductRawMaterialNeededRepository
    {
        // Get by ID
        public static ProductRawMaterialNeededDTO GetProductRawMaterialNeeded(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterialNeeded = context.ProductRawMaterialNeeded
                                               .Include(r => r.RawMaterial)
                                               .FirstOrDefault(r => r.Id == id);

                return rawMaterialNeeded == null ? null : ProductRawMaterialNeededMapper.Map(rawMaterialNeeded);
            }
        }

        public static List<ProductRawMaterialNeededDTO> GetProductRawMaterialNeededFromProduct(ProductDTO product)
        {
            Console.WriteLine(product.Id);
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterialsNeeded = context.ProductRawMaterialNeeded
                                                .Include(r => r.RawMaterial)
                                                .Where(r => r.ProductId == product.Id)
                                                .AsEnumerable();

                Console.WriteLine(rawMaterialsNeeded);
                var test = rawMaterialsNeeded.Select(r => ProductRawMaterialNeededMapper.Map(r)).ToList();
                Console.WriteLine(test);

                return rawMaterialsNeeded.Select(r => ProductRawMaterialNeededMapper.Map(r)).ToList();
            }
        }

        // Get all
        public static List<ProductRawMaterialNeededDTO> GetAllProductRawMaterialsNeeded()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterialsNeeded = context.ProductRawMaterialNeeded
                                                .Include(r => r.RawMaterial) // Include RawMaterial if needed
                                                .AsEnumerable();

                return rawMaterialsNeeded.Select(r => ProductRawMaterialNeededMapper.Map(r)).ToList();
            }
        }

        // Get by RawMaterial name
        public static List<ProductRawMaterialNeededDTO> GetByRawMaterialName(string rawMaterialName)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterialsNeeded = context.ProductRawMaterialNeeded
                                                .Include(r => r.RawMaterial)
                                                .Where(r => r.RawMaterial.Name.Contains(rawMaterialName))
                                                .AsEnumerable();

                return rawMaterialsNeeded.Select(r => ProductRawMaterialNeededMapper.Map(r)).ToList();
            }
        }

        // Add
        public static ProductRawMaterialNeededDTO AddProductRawMaterialNeeded(ProductRawMaterialNeededDTO rawMaterialNeededDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterialNeeded = ProductRawMaterialNeededMapper.Map(rawMaterialNeededDTO);
                context.ProductRawMaterialNeeded.Add(rawMaterialNeeded);
                context.SaveChanges();

                return ProductRawMaterialNeededMapper.Map(rawMaterialNeeded);
            }
        }

        // Edit / Update
        public static ProductRawMaterialNeededDTO EditProductRawMaterialNeeded(ProductRawMaterialNeededDTO rawMaterialNeededDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var dataRawMaterialNeeded = context.ProductRawMaterialNeeded
                                                   .Include(r => r.RawMaterial)
                                                   .FirstOrDefault(r => r.Id == rawMaterialNeededDTO.Id);

                if (dataRawMaterialNeeded == null)
                {
                    throw new Exception("ProductRawMaterialNeeded not found.");
                }

                ProductRawMaterialNeededMapper.Update(rawMaterialNeededDTO, dataRawMaterialNeeded);
                context.SaveChanges();

                return rawMaterialNeededDTO;
            }
        }

        // Delete
        public static ProductRawMaterialNeededDTO DeleteProductRawMaterialNeeded(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterialNeeded = context.ProductRawMaterialNeeded
                                               .FirstOrDefault(r => r.Id == id);

                if (rawMaterialNeeded == null)
                {
                    throw new Exception("ProductRawMaterialNeeded not found.");
                }

                context.ProductRawMaterialNeeded.Remove(rawMaterialNeeded);
                context.SaveChanges();

                return ProductRawMaterialNeededMapper.Map(rawMaterialNeeded);
            }
        }
    }
}