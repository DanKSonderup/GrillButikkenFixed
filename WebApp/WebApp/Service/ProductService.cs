using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class ProductService
    {

        public static ProductDTO CreateProduct(int id, string name, TimeSpan estimatedProductionTime, Dictionary<RawMaterial, double> rawMaterialNeeded, DateTime createdAt, DateTime updatedAt, int amountInStock)
        {
            return ProductRepository.AddProduct(new ProductDTO(id, name, estimatedProductionTime, rawMaterialNeeded, createdAt, updatedAt, amountInStock));
        }

        public static ProductDTO DeleteProduct(ProductDTO productDTO)
        {
            return ProductRepository.DeleteProduct(productDTO);
        }

        public static IEnumerable<ProductDTO> GetAllProducts()
        {
            return ProductRepository.GetProducts();
        }

        public static IEnumerable<ProductDTO> GetProductByName(string name)
        {
            return ProductRepository.GetProduct(name);
        }

        public static ProductDTO UpdateProduct(ProductDTO productDTO)
        {
            return ProductRepository.EditProduct(productDTO);
        }
    }
}