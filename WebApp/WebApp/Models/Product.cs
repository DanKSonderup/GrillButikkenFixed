using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace WebApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan EstimatedProductionTime { get; set; }
        public virtual List<ProductRawMaterialNeeded> ProductRawMaterialNeeded { get; set; } = new List<ProductRawMaterialNeeded>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int AmountInStock { get; set; }

        public Product(string name, TimeSpan estimatedProductionTime, DateTime createdAt, DateTime updatedAt, int amountInStock)
        {
            Name = name;
            EstimatedProductionTime = estimatedProductionTime;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            AmountInStock = amountInStock;
        }
        public Product() { }

        public void AddMaterial(RawMaterial rawMaterial, double amount)
        {
            ProductRawMaterialNeeded.Add(new Models.ProductRawMaterialNeeded(rawMaterial, amount));
        }

        public void RemoveMaterial(ProductRawMaterialNeeded rawMaterial)
        {
            ProductRawMaterialNeeded.Remove(rawMaterial);
        }


        public override string ToString()
        {
            return Name + ": " + EstimatedProductionTime + ". Created at: " + CreatedAt + ". Updated at: " + UpdatedAt;
        }
    }
}