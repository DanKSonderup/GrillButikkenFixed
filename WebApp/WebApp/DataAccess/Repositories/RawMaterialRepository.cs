using System;
using System.Collections.Generic;
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

        public List<RawMaterialDTO> GetRawMaterial(string name)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.RawMaterials.Where(r => r.Name == name).Select(r => RawMaterialMapper.Map(r)).ToList();
            }
        }

        public List<RawMaterialDTO> GetRawMaterials()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                return context.RawMaterials.Select(r => RawMaterialMapper.Map(r)).ToList();
            }
        }

        // Add
        public RawMaterialDTO AddRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.RawMaterials.Add(RawMaterialMapper.Map(rawDTO));
                context.SaveChanges();
            }
            return rawDTO;
        }

        // Edit / update
        public RawMaterialDTO EditRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                RawMaterial dataRawMaterial = context.RawMaterials.Find(rawDTO.Material_id);
                RawMaterialMapper.Update(rawDTO, dataRawMaterial);

                context.SaveChanges();
            }
            return rawDTO;
        }

        public RawMaterialDTO DeleteRawMaterial(RawMaterialDTO rawDTO)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                context.RawMaterials.Remove(RawMaterialMapper.Map(rawDTO));
            }
            return rawDTO;
        }

    }
}