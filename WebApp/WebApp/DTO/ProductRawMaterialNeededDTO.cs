using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class ProductRawMaterialNeededDTO
    {
        public int Id { get; set; } 
        public RawMaterial RawMaterial { get; set; }
        public double Quantity { get; set; }
        public string RawMaterialName { get; set; }
    }
}