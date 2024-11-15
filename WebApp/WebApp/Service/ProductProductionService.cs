using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.Models;

namespace WebApp.Service
{
    public class ProductProductionService
    {
        public ProductProduction GetProductProductionById(Guid guid)
        {

            // ProductProduction productionProject = await _productionProjectRepository.GetById(guid);

            return new ProductProduction();
        }

        public IEnumerable<ProductProduction> GetAllProductProductions()
        {
            return new List<ProductProduction>();
        }

        public void CreateProductProduction() // Husk at tilføje input params
        {
            // ProductProduction productionProject = await _productionProjectRepository.CreateProductProduction(InputParams);
        }

        public void UpdateProductProduction(ProductProduction productionProject)
        {
            // ProductProduction productionProject = await _productionProjectRepository.UpdateProductProduction(InputParams);
        }

        public void UpdateStatusForProductProduction(string status, Guid guid)
        {
            // Update the task value for a specific ProductProduction
        }

        public void DeleteProductProduction(Guid guid)
        {
            // ProductProduction productionProject = await _productionProjectRepository.DeleteProductProduction(Guid);
        }
    }
}