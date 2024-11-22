using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class ProductProductionService
    {
        public static ProductProductionDTO GetProductProductionByName(string productionName)
        {
            return ProductProductionRepository.GetProductProduction(productionName);
        }

        public static ProductProductionDTO GetProductProductionById(int id)
        {
            return ProductProductionRepository.GetProductProduction(id);
        }

        public static List<ProductProductionDTO> GetAllProductProductions()
        {
            return ProductProductionRepository.GetProductProductions();
        }

        public static ProductProductionDTO CreateProductProduction(string projectName, Product product, int quantityToProduce, DateTime createdAt, DateTime deadline, Status status) // Husk at tilføje input params
        {
            return ProductProductionRepository.AddProductProduction(new ProductProductionDTO(projectName, product, quantityToProduce, createdAt, deadline, status));
        }

        public static ProductProductionDTO UpdateProductProduction(ProductProductionDTO PPDTO)
        {
            return ProductProductionRepository.EditProductProduction(PPDTO);
        }

        public static ProductProductionDTO UpdateStatusForProductProduction(Status status, ProductProductionDTO PPDTO)
        {
            return ProductProductionRepository.UpdateProductProductionStatus(status, PPDTO);
        }

        public static void DeleteProductProduction(ProductProductionDTO PPDTO)
        {
            ProductProductionRepository.DeleteProductProduction(PPDTO);
        }

        public static bool IsDuplicateName(string name)
        {
            List<ProductProductionDTO> pp = GetAllProductProductions();

            return pp.Any(p => p.ProjectName.Equals(name));
        }
    }
}