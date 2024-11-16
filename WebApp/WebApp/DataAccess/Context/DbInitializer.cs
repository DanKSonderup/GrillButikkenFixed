using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.Models;

namespace WebApp.DataAccess.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            // Guid projectId, string projectName, int quantityToProduce,DateTime createdAt, DateTime deadline, TimeSpan timeSpent, Status status
            context.Biler.Add(new Models.Bil(1, "Honda", DateTime.Now));
            context.RawMaterials.Add(new Models.RawMaterial("Grillspyd", new Models.MeasurementType("kg"), 20));
            context.RawMaterials.Add(new Models.RawMaterial("Jernstang", new Models.MeasurementType("kg"), 20));
            context.RawMaterials.Add(new Models.RawMaterial("Jernstang3", new Models.MeasurementType("kg"), 20));

            Dictionary<Models.RawMaterial, double> materialsNeeded = new Dictionary<Models.RawMaterial, double>();
            RawMaterial testMaterial = new RawMaterial("Fuglevinger", new MeasurementType("kg"), 20);
            materialsNeeded.Add(testMaterial, 10);
            context.Products.Add(new Models.Product(1, "Grillspyd", new TimeSpan(0, 0, 0), materialsNeeded, DateTime.Now, DateTime.Now, 20));

            context.SaveChanges();

            base.Seed(context);
        }

        private void dummy()
        {
            string result = System.Data.Entity.SqlServer.SqlFunctions.Char(65);
        }
    }
}