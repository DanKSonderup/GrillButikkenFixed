﻿using System;
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
                // Hent data fra databasen først
                var measurementTypes = context.MeasurementTypes.ToList();

                // Udfør mapping i hukommelsen
                return measurementTypes.Select(r => MeasurementTypeMapper.Map(r)).ToList();
            }
        }

        public static MeasurementTypeDTO GetMeasurementTypeByName(string name)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var measurementType = context.MeasurementTypes.FirstOrDefault(r => r.Name.Equals(name));
                Console.WriteLine(measurementType);
                return MeasurementTypeMapper.Map(measurementType);
            }
        }

        public static MeasurementTypeDTO AddMeasurementType(MeasurementTypeDTO measurementTypeDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var measurementType = MeasurementTypeMapper.Map(measurementTypeDTO);
                context.MeasurementTypes.Add(measurementType);
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