using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class ProductProductionService
    {
        public static IEnumerable<ProductProductionDTO> GetProductProductionByName(string productionName)
        {
            return ProductProductionRepository.GetProductProduction(productionName);
        }

        public static IEnumerable<ProductProductionDTO> GetAllProductProductions()
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
        public static ProductProductionDTO DeleteProductProduction(ProductProductionDTO PPDTO)
        {
            return ProductProductionRepository.DeleteProductProduction(PPDTO);
        }
    }
}