using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebApp.Models;

namespace WebApp.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("Grillbutikken") { }

        public DbSet<Bil> Biler { get; set; }
        public DbSet<RawMaterial> RawMaterials { get; set; }
        public DbSet<ProductProduction> ProductProductions { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}