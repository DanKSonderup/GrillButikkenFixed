using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan EstimatedProductionTime { get; set; }
        public List<ProductRawMaterialNeeded> ProductRawMaterialNeeded { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AmountInStock { get; set; }

        public ProductDTO()
        {
        }

        public ProductDTO(string name, TimeSpan estimatedProductionTime, DateTime createdAt, DateTime updatedAt, int amountInStock)
        {
            Name = name;
            EstimatedProductionTime = estimatedProductionTime;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            AmountInStock = amountInStock;
        }


    }
}