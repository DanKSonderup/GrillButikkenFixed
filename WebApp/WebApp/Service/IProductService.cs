using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    internal interface IProductService
    {
        ProductDTO CreateProduct(int id, string name, TimeSpan estimatedProductionTime, Dictionary<RawMaterial, double> rawMaterialNeeded, DateTime createdAt, DateTime updatedAt, int amountInStock);
        IEnumerable<ProductDTO> GetProductByName(string name);
        IEnumerable<ProductDTO> GetAllProducts();
        ProductDTO UpdateProduct(ProductDTO productDTO);
        ProductDTO DeleteProduct(ProductDTO productDTO);
    }
}