using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO
{
    public class RawMaterialDTO
    {
        [Key]
        public int Material_id { get; set; } // Key og GUID
        [Required]
        public string Name { get; set; } // Required og VARCHAR

        [Required]
        public MeasurementType MeasurementType { get; set; }
        public virtual List<RawMaterialStockDTO> Stocks { get; set; }
        public RawMaterialDTO()
        {
            Stocks = new List<RawMaterialStockDTO>();
        }

        public RawMaterialDTO(string name, MeasurementType measurementType, double amount, DateTime? expirationDate = null)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = new List<RawMaterialStockDTO>
            {
                new RawMaterialStockDTO(Material_id, amount, expirationDate)
            }; ;
        }
        public RawMaterialDTO(string name, MeasurementType measurementType, double amount)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = new List<RawMaterialStockDTO>
            {
                new RawMaterialStockDTO(Material_id, amount)
            };
        }
        public RawMaterialDTO(string name, MeasurementType measurementType)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = new List<RawMaterialStockDTO>();
        }

        public RawMaterialDTO(string name, MeasurementType measurementType, List<RawMaterialStockDTO> stocks)
        {
            Name = name;
            MeasurementType = measurementType;
            Stocks = stocks;
        }

        public void AddStock(double amount, DateTime? expirationDate = null)
        {
            Stocks.Add(new RawMaterialStockDTO(Material_id, amount, expirationDate));
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

        public double FullAmount()
        {
            return Stocks.Sum(rm => rm.Amount);
        }

        public DateTime? GetClosestExpirationDate()
        {
            return Stocks
                .Where(stock => stock.ExpirationDate.HasValue)
                .OrderBy(stock => stock.ExpirationDate)
                .Select(stock => stock.ExpirationDate)
                .FirstOrDefault();
        }
    }

    public class RawMaterialStockDTO
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int RawMaterialId { get; set; }

        public RawMaterialStockDTO() { }
        public RawMaterialStockDTO(int rawMaterialId, double amount, DateTime? expirationDate = null)
        {
            RawMaterialId = rawMaterialId;
            Amount = amount;
            ExpirationDate = expirationDate;
        }
    }

}