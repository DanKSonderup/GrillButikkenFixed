﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.DTO.Mappers
{
    public class RawMaterialMapper
    {
        public static RawMaterialDTO Map(RawMaterial rawMaterial)
        {
            if (rawMaterial != null)
                return new RawMaterialDTO(rawMaterial.Name, rawMaterial.MeasurementType, rawMaterial.MeasurementValue);
            else
                return null;
        }
        public static RawMaterial Map(RawMaterialDTO rawDTO)
        {
            if (rawDTO != null)
                return new RawMaterial(rawDTO.Name, rawDTO.MeasurementType, rawDTO.MeasurementValue);
            else
                return null;
        }

        internal static void Update(RawMaterialDTO rawDTO, RawMaterial rawMaterial)
        {
            if (rawDTO != null)
            {
                rawMaterial.Name = rawDTO.Name;
                rawMaterial.MeasurementType = rawDTO.MeasurementType;
                rawMaterial.MeasurementValue = rawDTO.MeasurementValue;
            }
            else
                rawMaterial = null;
        }

        private static List<RawMaterialDTO> Map(List<RawMaterial> rawMaterials)
        {
            List<RawMaterialDTO> retur = new List<RawMaterialDTO>();
            foreach (RawMaterial rawMaterial in rawMaterials)
            {
                retur.Add(RawMaterialMapper.Map(rawMaterial));
            }
            return retur;
        }
    }
}