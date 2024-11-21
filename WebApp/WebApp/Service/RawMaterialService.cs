using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Service
{
    public class RawMaterialService
    {
        public static RawMaterialDTO CreateRawMaterial(string name, MeasurementType measurementType)
        {
            return RawMaterialRepository.AddRawMaterial(new RawMaterialDTO(name, measurementType));
        }

        public static void DeleteRawMaterial(int id)
        {
            RawMaterialRepository.DeleteRawMaterial(id);
        }

        public static List<RawMaterialDTO> GetAllRawMaterials()
        {
            return RawMaterialRepository.GetRawMaterials();
        }

        public static List<RawMaterialDTO> GetRawMaterialByName(string name)
        {
            return RawMaterialRepository.GetRawMaterial(name);
        }

        public static RawMaterialDTO GetRawMaterialById(int id)
        {
            return RawMaterialRepository.GetRawMaterialById(id);
        }

        public static RawMaterialDTO UpdateRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            return RawMaterialRepository.EditRawMaterial(rawMaterialDTO);
        }

        public static void AddStockToRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            RawMaterialRepository.AddStockToRawMaterial(rawMaterialDTO);
        }


        /// Other methods
        
        public static bool IsDuplicateName(string name)
        {
            List<RawMaterialDTO> raws = GetAllRawMaterials();

            return raws.Any(raw => raw.Name.Equals(name));
        }
    }
}