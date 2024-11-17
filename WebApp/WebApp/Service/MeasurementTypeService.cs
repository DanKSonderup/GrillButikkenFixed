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
            return MeasurementTypeRepository.GetMeasurementTypeByName(name);
        }

        public static MeasurementTypeDTO CreateMeasurementType(string name)
        {

            var measurementTypeDTO = new MeasurementTypeDTO(name);
            return MeasurementTypeRepository.AddMeasurementType(measurementTypeDTO);
        }

        public static MeasurementTypeDTO DeleteMeasurementType(string name)
        {
            return MeasurementTypeRepository.DeleteMeasurementType(new MeasurementTypeDTO(name));
        }


    }
}