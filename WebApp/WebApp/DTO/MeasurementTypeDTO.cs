﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class MeasurementTypeDTO
    {
        public string Name { get; set; }

        public MeasurementTypeDTO(string name)
        {
            Name = name;
        }
    }
}