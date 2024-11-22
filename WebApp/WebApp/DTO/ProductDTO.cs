using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan EstimatedProductionTime { get; set; }
        public Dictionary<RawMaterial, double> RawMaterialNeeded { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProductDTO(int id, string name, TimeSpan estimatedProductionTime, Dictionary<RawMaterial, double> rawMaterialNeeded, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            EstimatedProductionTime = estimatedProductionTime;
            RawMaterialNeeded = rawMaterialNeeded;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}