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
        public int Material_id { get; set; } // Key og GUID
        public String Name { get; set; } // Required og VARCHAR
        public double MeasurementValue { get; set; } // Required
        //[ForeignKey("MeasurementType")] Bør den ikke have det?
        public MeasurementType MeasurementType { get; set; }
        public DateTime ExpirationDate { get; set; }

        public RawMaterial(string name, MeasurementType measurementType, double measurementValue)
        {
            Name = name;
            MeasurementType = measurementType;
            MeasurementValue = measurementValue;
            ExpirationDate = DateTime.Now.AddYears(30);
        }

        public RawMaterial(string name, MeasurementType measurementType, double measurementValue, DateTime expirationDate)
        {
            Name = name;
            MeasurementType = measurementType;
            MeasurementValue = measurementValue;
            ExpirationDate = expirationDate;
        }

        public override string ToString()
        {
            return Name + ": " + MeasurementValue + MeasurementType + ". Expiration: " + ExpirationDate;
        }
    }
}