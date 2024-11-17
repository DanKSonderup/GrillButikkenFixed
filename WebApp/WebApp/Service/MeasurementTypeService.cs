using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;

namespace WebApp.Service
{
    public class MeasurementTypeService
    {
        public static List<MeasurementTypeDTO> GetAllMeasurementTypes()
        {
            return MeasurementTypeRepository.GetMeasurementTypes();
        }

        public static MeasurementTypeDTO GetMeasurementTypeByName(string name)
        {
            var test = MeasurementTypeRepository.GetMeasurementTypeByName(name);
            Console.WriteLine(test);
            return MeasurementTypeRepository.GetMeasurementTypeByName(name);
        }

        public static MeasurementTypeDTO CreateMeasurementType(string name)
        {

            var measurementTypeDTO = new MeasurementTypeDTO
            {
                Name = name
            };
            return MeasurementTypeRepository.AddMeasurementType(measurementTypeDTO);
        }

        public static MeasurementTypeDTO DeleteMeasurementType(string name)
        {
            var measurementTypeDTO = new MeasurementTypeDTO
            {
                Name = name
            };
            return MeasurementTypeRepository.DeleteMeasurementType(measurementTypeDTO);
        }


    }
}