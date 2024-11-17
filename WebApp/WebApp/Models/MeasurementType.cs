using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MeasurementType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public MeasurementType() { }
        public MeasurementType(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}