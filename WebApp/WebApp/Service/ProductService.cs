using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class ProductService : IProductService
    {

        public ProductDTO CreateProduct(int id, string name, TimeSpan estimatedProductionTime, Dictionary<RawMaterial, double> rawMaterialNeeded, DateTime createdAt, DateTime updatedAt, int amountInStock)
        {
            return ProductRepository.AddProduct(new ProductDTO(id, name, estimatedProductionTime, rawMaterialNeeded, createdAt, updatedAt, amountInStock));
        }

        public ProductDTO DeleteProduct(ProductDTO productDTO)
        {
            return ProductRepository.DeleteProduct(productDTO);
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return ProductRepository.GetProducts();
        }

        public IEnumerable<ProductDTO> GetProductByName(string name)
        {
            return ProductRepository.GetProduct(name);
        }

        public ProductDTO UpdateProduct(ProductDTO productDTO)
        {
            return ProductRepository.EditProduct(productDTO);
        }
    }
}