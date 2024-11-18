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
        public static RawMaterialDTO CreateRawMaterial(string name, MeasurementType measurementType, double amount, DateTime? expirationDate)
        {
            return RawMaterialRepository.AddRawMaterial(new RawMaterialDTO(name, measurementType, amount, expirationDate));
        }

        public static RawMaterialDTO DeleteRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            return RawMaterialRepository.DeleteRawMaterial(rawMaterialDTO);
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


        /// Other methods
        
        public static bool IsDuplicateName(string name)
        {
            List<RawMaterialDTO> raws = GetAllRawMaterials();

            return raws.Any(raw => raw.Name.Equals(name));
        }
    }
}