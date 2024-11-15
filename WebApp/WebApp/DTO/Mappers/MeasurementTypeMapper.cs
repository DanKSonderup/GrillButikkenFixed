using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class MeasurementTypeMapper
    {

        public static MeasurementTypeDTO Map(MeasurementType measurementType)
        {
            if (measurementType == null) return null;

            return new MeasurementTypeDTO(measurementType.Name);
        }

        public static MeasurementType Map(MeasurementTypeDTO measurementTypeDTO)
        {
            if (measurementTypeDTO == null) return null;

            return new MeasurementType(measurementTypeDTO.Name);
        }
    }
}