using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.DataAccess.Context
{
    public class DbInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            // Guid projectId, string projectName, int quantityToProduce,DateTime createdAt, DateTime deadline, TimeSpan timeSpent, Status status
            context.Biler.Add(new Models.Bil(1, "Honda", DateTime.Now));
            MeasurementType kg = new Models.MeasurementType("kg");
            context.MeasurementTypes.Add(kg);
            context.RawMaterials.Add(new Models.RawMaterial("Grillspyd", kg, 20));
            context.RawMaterials.Add(new Models.RawMaterial("Jernstang", kg, 20));
            context.RawMaterials.Add(new Models.RawMaterial("Jernstang3", kg, 20));

            context.SaveChanges();

            base.Seed(context);
        }

        private void dummy()
        {
            string result = System.Data.Entity.SqlServer.SqlFunctions.Char(65);
        }
    }
}