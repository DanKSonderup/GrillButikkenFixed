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
        RawMaterialRepository rawMaterialRepository = new RawMaterialRepository();
        public RawMaterialDTO CreateRawMaterial(string name, MeasurementType measurementType, double measurementValue)
        {
            return rawMaterialRepository.AddRawMaterial(new RawMaterialDTO(name, measurementType, measurementValue));
        }

        public RawMaterialDTO DeleteRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            return rawMaterialRepository.DeleteRawMaterial(rawMaterialDTO);
        }

        public List<RawMaterialDTO> GetAllRawMaterials()
        {
            return rawMaterialRepository.GetRawMaterials();
        }

        public List<RawMaterialDTO> GetRawMaterialByName(string name)
        {
            return rawMaterialRepository.GetRawMaterial(name);
        }

        public RawMaterialDTO UpdateRawMaterial(RawMaterialDTO rawMaterialDTO)
        {
            return rawMaterialRepository.EditRawMaterial(rawMaterialDTO);
        }
    }
}