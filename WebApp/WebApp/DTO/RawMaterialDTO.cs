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
        public String Name { get; set; } // Required og VARCHAR
        [Required]
        public double MeasurementValue { get; set; } // Required
        [Required]
        public MeasurementType MeasurementType { get; set; }
        public DateTime ExpirationDate { get; set; }

        public RawMaterialDTO()
        {
        }

        public RawMaterialDTO(string name, MeasurementType measurementType, double measurementValue)
        {
            Name = name;
            MeasurementType = measurementType;
            MeasurementValue = measurementValue;
            ExpirationDate = DateTime.Now.AddYears(30);
        }

        public RawMaterialDTO(string name, MeasurementType measurementType, double measurementValue, DateTime expirationDate)
        {
            Name = name;
            MeasurementType = measurementType;
            MeasurementValue = measurementValue;
            ExpirationDate = expirationDate;
        }
    }
}