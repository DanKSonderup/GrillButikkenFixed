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
                var rawMaterials = context.RawMaterials
                    .Include(rm => rm.MeasurementType)
                    .ToList();

                return rawMaterials.Where(r => r.Name == name).Select(r => RawMaterialMapper.Map(r)).ToList();
            }
        }

        public static RawMaterialDTO GetRawMaterialById(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterials = context.RawMaterials
                    .Include(rm => rm.MeasurementType)
                    .ToList();


                var rawMat = rawMaterials.Where(r => r.Material_id == id).First();

                return RawMaterialMapper.Map(rawMat);
            }
        }


        public static List<RawMaterialDTO> GetRawMaterials()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                var rawMaterials = context.RawMaterials
                    .Include(rm => rm.MeasurementType)
                    .ToList();

                return rawMaterials.Select(r => RawMaterialMapper.Map(r)).ToList();
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
                    Stocks = RawMaterialStockMapper.Map(rawDTO.Stocks)
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
                // Find det eksisterende RawMaterial
                RawMaterial dataRawMaterial = context.RawMaterials
                    .Include(r => r.MeasurementType) // Sørg for at hente MeasurementType med
                    .FirstOrDefault(r => r.Material_id == rawDTO.Material_id);

                if (dataRawMaterial == null)
                {
                    throw new Exception("RawMaterial not found.");
                }

                // Find det eksisterende MeasurementType i databasen
                var existingMeasurementType = context.MeasurementTypes
                    .FirstOrDefault(mt => mt.Name == rawDTO.MeasurementType.Name);

                if (existingMeasurementType == null)
                {
                    throw new Exception("MeasurementType does not exist.");
                }

                // Opdater RawMaterial med de nye værdier
                dataRawMaterial.Name = rawDTO.Name;
                dataRawMaterial.MeasurementType = existingMeasurementType; // Brug det eksisterende MeasurementType
                dataRawMaterial.Stocks.Add(RawMaterialStockMapper.Map(rawDTO.Stocks.Last()));


                // Markér RawMaterial som ændret, hvis det er nødvendigt (efter opdateringen)
                context.Entry(dataRawMaterial).State = EntityState.Modified;


                // Gem ændringerne i databasen
                context.SaveChanges();
            }

            // Returnér DTO'en efter ændringerne er blevet gemt
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