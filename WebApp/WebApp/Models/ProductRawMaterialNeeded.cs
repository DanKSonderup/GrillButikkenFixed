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
        public int Id { get; set; } 

        [ForeignKey("RawMaterial")]
        public int RawMaterialId { get; set; } 
        public virtual RawMaterial RawMaterial { get; set; }

        public double Quantity { get; set; } 

        [ForeignKey("Product")]
        public int ProductId { get; set; } 
        public virtual Product Product { get; set; }

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