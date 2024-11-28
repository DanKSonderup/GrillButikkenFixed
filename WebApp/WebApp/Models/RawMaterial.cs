using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RawMaterial
    {
        [Key]
        public int Material_id { get; set; } 
        public string Name { get; set; }
        public MeasurementType MeasurementType { get; set; }

        public virtual List<RawMaterialStock> Stocks { get; set; }

        public RawMaterial()
        {
            Stocks = new List<RawMaterialStock>();
        }   

        public RawMaterial(string name, MeasurementType measurementType, double amount, DateTime expirationDate)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = new List<RawMaterialStock>
            {
                new RawMaterialStock(Material_id, amount, expirationDate)
            };
        }

        public RawMaterial(string name, MeasurementType measurementType, double amount)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = new List<RawMaterialStock>
            {
                new RawMaterialStock(Material_id, amount)
            };
        }

        public RawMaterial(string name, MeasurementType measurementType, List<RawMaterialStock> stocks)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = stocks;
        }

        public void AddStock(double amount, DateTime expirationDate)
        {
            Stocks.Add(new RawMaterialStock(Material_id, amount, expirationDate));
        }

        public void AddStock(double amount)
        {
            Stocks.Add(new RawMaterialStock(Material_id, amount));
        }

        public void RemoveStock(double amount, int id)
        {
            var rawMatStock = Stocks.Find(rm => rm.Id == id);

            if (rawMatStock != null)
            {
                rawMatStock.Amount -= amount;
                if (rawMatStock.Amount < 0)
                {
                    Stocks.Remove(rawMatStock);
                }
            }
        }
    }

    public class  RawMaterialStock
    {
        [Key]
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public int RawMaterialId { get; set; }

        public RawMaterialStock() { }
        public RawMaterialStock(int rawMaterialId, double amount, DateTime? expirationDate = null)
        {
            RawMaterialId = rawMaterialId;
            Amount = amount;
            ExpirationDate = expirationDate;
        }
    }
}