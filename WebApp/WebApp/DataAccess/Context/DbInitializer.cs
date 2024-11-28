using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Service;
using WebApp.DataAccess.Repositories;
using WebApp.Models;

namespace WebApp.DataAccess.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            MeasurementType kg = new MeasurementType("Kg");
            context.MeasurementTypes.Add(kg);
            /**
            // Add MeasurementType
            MeasurementType kg = new MeasurementType("Kg");
            context.MeasurementTypes.Add(kg);

            // Add RawMaterials
            RawMaterial jernstang = new RawMaterial("Jernstang", kg, 20);
            RawMaterial jernstang2 = new RawMaterial("Jernstang2", kg, 20);

            context.RawMaterials.Add(jernstang);
            context.RawMaterials.Add(jernstang2);

            // Create Products and their associated ProductRawMaterialNeeded
            var product1 = new Product("Grillspyd", TimeSpan.Zero, DateTime.Now, DateTime.Now, 20);
            var product2 = new Product("BageEnzym", TimeSpan.Zero, DateTime.Now, DateTime.Now, 20);
            var product3 = new Product("Grillrist", TimeSpan.Zero, DateTime.Now, DateTime.Now, 20);

            // Add ProductRawMaterialNeeded for each Product
            product1.ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>
            {
                new ProductRawMaterialNeeded { RawMaterial = jernstang, Quantity = 2 }
            };

            product2.ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>
            {
                new ProductRawMaterialNeeded { RawMaterial = jernstang, Quantity = 3 }
            };

            product3.ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>
            {
                new ProductRawMaterialNeeded { RawMaterial = jernstang2, Quantity = 5 }
            };

            // Add Products to DbContext
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);

            // Save Changes
            */
            context.SaveChanges();
        }

        private void dummy()
        {
            string result = System.Data.Entity.SqlServer.SqlFunctions.Char(65);
        }
    }
}