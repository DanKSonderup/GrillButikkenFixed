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


                var rawMat = rawMaterials.FirstOrDefault(r => r.Material_id == id);

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
                };


                context.RawMaterials.Add(newRawMaterial);
                context.SaveChanges();
            }
            return rawDTO;
        }

        public static RawMaterialDTO EditRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                RawMaterial dataRawMaterial = context.RawMaterials
                    .Include(r => r.MeasurementType)
                    .FirstOrDefault(r => r.Material_id == rawDTO.Material_id);

                if (dataRawMaterial == null)
                {
                    throw new Exception("RawMaterial not found.");
                }

                var existingMeasurementType = context.MeasurementTypes
                    .FirstOrDefault(mt => mt.Name == rawDTO.MeasurementType.Name);

                if (existingMeasurementType == null)
                {
                    throw new Exception("MeasurementType does not exist.");
                }

                dataRawMaterial.Name = rawDTO.Name;
                dataRawMaterial.MeasurementType = existingMeasurementType;

                context.Entry(dataRawMaterial).State = EntityState.Modified;

                context.SaveChanges();
            }

            return rawDTO;
        }

        public static void AddStockToRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                RawMaterial dataRawMaterial = context.RawMaterials
                    .Include(r => r.Stocks)
                    .FirstOrDefault(r => r.Material_id == rawDTO.Material_id);

                if (dataRawMaterial == null)
                {
                    throw new Exception("RawMaterial not found.");
                }

                var newStock = RawMaterialStockMapper.Map(rawDTO.Stocks.Last()); 
                dataRawMaterial.Stocks.Add(newStock);

                context.SaveChanges();
            }
        }



        public static void DeleteRawMaterial(int id)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                
                var rawMaterial = context.RawMaterials.Find(id);
                Console.WriteLine(rawMaterial);
                var stock = context.RawMaterialsStock
                    .Where(s => s.RawMaterialId == id)
                    .ToList();

                stock.ForEach(s => context.RawMaterialsStock.Remove(s));

                if (rawMaterial != null)
                {
                    context.RawMaterials.Remove(rawMaterial);
                    context.SaveChanges();
                }
            }
        }

    }
}