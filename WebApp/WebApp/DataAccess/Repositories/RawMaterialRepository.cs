using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Context;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Models;

namespace WebApp.DataAccess.Repositories
{
    public class RawMaterialRepository
    {
        // Create, Delete, Edit, Get, GetAll, Add

        public static List<RawMaterialDTO> GetRawMaterial(string name)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.RawMaterials.Where(r => r.Name == name).Select(r => RawMaterialMapper.Map(r)).ToList();
            }
        }

        public static List<RawMaterialDTO> GetRawMaterials()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.RawMaterials.Select(r => RawMaterialMapper.Map(r)).ToList();
            }
        }

        // Add
        public static RawMaterialDTO AddRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var existingMeasurementType = context.MeasurementTypes
                                            .SingleOrDefault(mt => mt.Name == rawDTO.MeasurementType.Name);

                if (existingMeasurementType == null)
                {
                    throw new Exception("There was an error finding the MeasurementType - Received MeasurementType does not exist");
                }

                context.Entry(existingMeasurementType).State = EntityState.Unchanged;

                var newRawMaterial = new RawMaterial
                {
                    Name = rawDTO.Name,
                    MeasurementType = existingMeasurementType,
                    MeasurementValue = rawDTO.MeasurementValue,
                    ExpirationDate = rawDTO.ExpirationDate
                };

                context.RawMaterials.Add(newRawMaterial);
                context.SaveChanges();
            }
            return rawDTO;
        }

        // Edit / update
        public static RawMaterialDTO EditRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                RawMaterial dataRawMaterial = context.RawMaterials.Find(rawDTO.Material_id);
                RawMaterialMapper.Update(rawDTO, dataRawMaterial);

                context.SaveChanges();
            }
            return rawDTO;
        }

        public static RawMaterialDTO DeleteRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.RawMaterials.Remove(RawMaterialMapper.Map(rawDTO));
            }
            return rawDTO;
        }

    }
}