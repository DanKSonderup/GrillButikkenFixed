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

        public static ProductDTO CreateProduct(string name, TimeSpan estimatedProductionTime, DateTime createdAt, DateTime updatedAt, int amountInStock)
        {
            return ProductRepository.AddProduct(new ProductDTO(name, estimatedProductionTime, createdAt, updatedAt, amountInStock));
        }

        public static ProductDTO DeleteProduct(ProductDTO productDTO)
        {
            return ProductRepository.DeleteProduct(productDTO);
        }

        public static List<ProductDTO> GetAllProducts()
        {
            return ProductRepository.GetProducts();
        }

        public static ProductDTO GetProductByName(string name)
        {
            return ProductRepository.GetProduct(name);
        }

        public static ProductDTO UpdateProduct(ProductDTO productDTO)
        {
            return ProductRepository.EditProduct(productDTO);
        }

        public IEnumerable<ProductDTO> GetAllProductsWithNameContaining(string input)
        {
            return ProductRepository.GetAllProductsWithNameContaining(input);
        }
    }
}