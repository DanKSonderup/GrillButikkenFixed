using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebApp.Models;

namespace WebApp.DataAccess.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {

        protected override void Seed(DatabaseContext context)
        {
            Console.WriteLine("Ayylmao");

            MeasurementType kg = new MeasurementType("kg");
            context.MeasurementTypes.Add(kg);

            RawMaterial rawMaterial1 = new RawMaterial("Grillspyd", kg, 20);
            RawMaterial rawMaterial2 = new RawMaterial("Jernstang", kg, 20);
            context.RawMaterials.Add(rawMaterial1);
            context.RawMaterials.Add(rawMaterial2);

            Dictionary<RawMaterial, double> materialsNeeded = new Dictionary<RawMaterial, double>
            {
                { rawMaterial1, 10 },
                { rawMaterial2, 5 }
            };

            Product product = new Product(1, "Grillspyd", new TimeSpan(0, 30, 0), materialsNeeded, DateTime.Now, DateTime.Now, 100);
            context.Products.Add(product);

            context.SaveChanges();

            ProductProduction productProduction = new ProductProduction("Grillspyd Produktion", product, 100, DateTime.Now, DateTime.Now.AddDays(10), Status.Completed);
            context.ProductProductions.Add(productProduction);

            context.SaveChanges();

            base.Seed(context);
        }

        private void dummy()
        {
            string result = System.Data.Entity.SqlServer.SqlFunctions.Char(65);
        }
    }
}