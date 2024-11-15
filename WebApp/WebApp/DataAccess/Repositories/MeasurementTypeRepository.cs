using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Context;
using WebApp.DTO;
using WebApp.DTO.Mappers;

namespace WebApp.DataAccess.Repositories
{
    public class MeasurementTypeRepository
    {
        public static List<MeasurementTypeDTO> GetMeasurementTypes()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.MeasurementTypes.Select(r => MeasurementTypeMapper.Map(r)).ToList();
            }
        }

        public static MeasurementTypeDTO AddMeasurementType(MeasurementTypeDTO measurementTypeDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.MeasurementTypes.Add(MeasurementTypeMapper.Map(measurementTypeDTO));
                context.SaveChanges();
            }
            return measurementTypeDTO;
        }

        public static MeasurementTypeDTO DeleteMeasurementType(MeasurementTypeDTO measurementTypeDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.MeasurementTypes.Remove(MeasurementTypeMapper.Map(measurementTypeDTO));
                context.SaveChanges();
            }
            return measurementTypeDTO;
        }


    }
}