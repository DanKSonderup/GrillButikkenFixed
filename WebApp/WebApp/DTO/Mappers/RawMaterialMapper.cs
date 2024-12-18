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
            {
                return new RawMaterialDTO
                {
                    Material_id = rawMaterial.Material_id,
                    Name = rawMaterial.Name,
                    MeasurementType = rawMaterial.MeasurementType,
                    Stocks = RawMaterialStockMapper.Map(rawMaterial.Stocks)
                };
            }

            else
                return null;
        }

        public static RawMaterial Map(RawMaterialDTO rawMaterial)
        {
            if (rawMaterial != null)
                return new RawMaterial(rawMaterial.Name, rawMaterial.MeasurementType, RawMaterialStockMapper.Map(rawMaterial.Stocks));
            else
                return null;
        }


        internal static void Update(RawMaterialDTO rawDTO, RawMaterial rawMaterial)
        {
            if (rawDTO != null)
            {
                rawMaterial.Name = rawDTO.Name;
                rawMaterial.MeasurementType = rawDTO.MeasurementType;
                rawMaterial.Stocks = RawMaterialStockMapper.Map(rawDTO.Stocks);
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