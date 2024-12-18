﻿using System;
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
        public static ProductDTO GetProduct(string name)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var product = context.Products
                                      .Where(r => r.Name == name)
                                      .FirstOrDefault(); 

                return product == null ? null : ProductMapper.Map(product);
            }
        }

        public static List<ProductDTO> GetProducts()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var products = context.Products.AsEnumerable();

                return products.Select(r => ProductMapper.Map(r)).ToList();
            }
        }

        public static List<ProductDTO> GetAllProductsWithNameContaining(string input)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.Products.AsEnumerable()
                              .Where(r => r.Name.Contains(input))
                              .Select(r => ProductMapper.Map(r))
                              .ToList();
            }
        }


        public static ProductDTO AddProduct(ProductDTO productDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.Products.Add(ProductMapper.Map(productDTO));

                foreach (var item in productDTO.ProductRawMaterialNeeded)
                {
                    context.ProductRawMaterialNeeded.Add(item);
                }
                context.SaveChanges();
            }
            return productDTO;

        }

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