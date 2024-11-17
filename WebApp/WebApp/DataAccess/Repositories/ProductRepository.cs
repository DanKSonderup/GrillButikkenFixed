using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Context;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public class ProductRepository
    {
        // Create, Delete, Edit, Get, GetAll, Add
        public static List<ProductDTO> GetProduct(string name)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Products.Where(r => r.Name == name).Select(r => ProductMapper.Map(r)).ToList();
            }
        }

        public static List<ProductDTO> GetProducts()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var products = context.Products.ToList();

                return products.Select(r => ProductMapper.Map(r)).ToList();
            }
        }


        // Add
        public static ProductDTO AddProduct(ProductDTO productDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Products.Add(ProductMapper.Map(productDTO));
                context.SaveChanges();
            }
            return productDTO;

        }

        // Edit / update
        public static ProductDTO EditProduct(ProductDTO productDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Product dataProduct = context.Products.Find(productDTO.Id);
                ProductMapper.Update(productDTO, dataProduct);

                context.SaveChanges();
            }
            return productDTO;
        }

        public static ProductDTO DeleteProduct(ProductDTO productDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Products.Remove(ProductMapper.Map(productDTO));
            }
            return productDTO;
        }

    }
}