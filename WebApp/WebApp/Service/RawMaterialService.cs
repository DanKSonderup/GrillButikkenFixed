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
        public static RawMaterialDTO CreateRawMaterial(string name, MeasurementType measurementType, double measurementValue)
        {
            return RawMaterialRepository.AddRawMaterial(new RawMaterialDTO(name, measurementType, measurementValue));
        }

        public static RawMaterialDTO DeleteRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            return RawMaterialRepository.DeleteRawMaterial(rawMaterialDTO);
        }

        public static IEnumerable<RawMaterialDTO> GetAllRawMaterials()
        {
            return RawMaterialRepository.GetRawMaterials();
        }

        public static IEnumerable<RawMaterialDTO> GetRawMaterialByName(string name)
        {
            return RawMaterialRepository.GetRawMaterial(name);
        }

        public static RawMaterialDTO UpdateRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            return RawMaterialRepository.EditRawMaterial(rawMaterialDTO);
        }
    }
}