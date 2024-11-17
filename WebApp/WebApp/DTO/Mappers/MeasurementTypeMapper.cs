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
            if (measurementType.Id == 0)
            {
                throw new Exception("YOU ARE RETARDED");
            }
            var measurementTypeDTO = new MeasurementTypeDTO
            {
                Id = measurementType.Id,
                Name = measurementType.Name
            };

            if (measurementTypeDTO.Id == 0)
            {
                throw new Exception("YOU ARE RETARDED");
            }

            Console.WriteLine(measurementTypeDTO);
            return measurementTypeDTO;
        }

        public static MeasurementType Map(MeasurementTypeDTO dto)
        {
            if (dto == null) return null;

            return new MeasurementType
            {
                Name = dto.Name
            };
        }
    }
}