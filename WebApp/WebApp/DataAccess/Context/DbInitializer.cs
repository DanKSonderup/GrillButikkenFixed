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

            MeasurementType liter = new MeasurementType("Liter");
            context.MeasurementTypes.Add(liter);

            RawMaterial milk = new RawMaterial("Mælk", liter, 5);
            RawMaterial cement = new RawMaterial("Cement", kg, 20);

            context.RawMaterials.Add(milk);
            context.RawMaterials.Add(cement);

            var pizzasten30 = new Product("Pizzasten 30cm", TimeSpan.FromHours(1), DateTime.Now, DateTime.Now, 20);
            var bageenzym = new Product("BageEnzym", TimeSpan.FromHours(10), DateTime.Now, DateTime.Now, 20);
            var pizzasten40 = new Product("Pizzasten 40cm", TimeSpan.FromHours(2), DateTime.Now, DateTime.Now, 20);

            pizzasten30.ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>
            {
                new ProductRawMaterialNeeded { RawMaterial = cement, Quantity = 2 }
            };

            bageenzym.ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>
            {
                new ProductRawMaterialNeeded { RawMaterial = milk, Quantity = 20 }
            };

            pizzasten40.ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>
            {
                new ProductRawMaterialNeeded {RawMaterial = cement, Quantity = 5}
            };

            context.Products.Add(pizzasten30);
            context.Products.Add(pizzasten40);
            context.Products.Add(bageenzym);

            ProductProduction production = new ProductProduction("Pizzasten 30cm", pizzasten30, 2, DateTime.Now, DateTime.Now.AddDays(30), Status.Waiting);

            context.ProductProductions.Add(production);

            // Save Changes
            context.SaveChanges();
        }

        private void dummy()
        {
            string result = System.Data.Entity.SqlServer.SqlFunctions.Char(65);
        }
    }
}