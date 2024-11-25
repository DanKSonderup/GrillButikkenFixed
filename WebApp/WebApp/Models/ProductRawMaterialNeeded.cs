using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class ProductRawMaterialNeeded
    {
        [Key]
        public int Id { get; set; } // Primary key for the junction table

        [ForeignKey("RawMaterial")]
        public int RawMaterialId { get; set; } // Foreign key to RawMaterial
        public virtual RawMaterial RawMaterial { get; set; }

        public double Quantity { get; set; } // Represents the amount needed

        [ForeignKey("Product")]
        public int ProductId { get; set; } // Foreign key to Product
        public virtual Product Product { get; set; } // Navigation property to Product

        public ProductRawMaterialNeeded()
        {
        }

        public ProductRawMaterialNeeded(RawMaterial rawMaterial, double quantity)
        {
            RawMaterial = rawMaterial;
            Quantity = quantity;
        }
    }
}