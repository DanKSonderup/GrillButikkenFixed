﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DTO.Mappers;
using WebApp.DTO;
using WebApp.Helpers;
using WebApp.Service;
using Microsoft.Ajax.Utilities;
using System.Web.WebPages;

namespace WebApp.Controllers
{
    public class RawMaterialsController : Controller
    {
        public ActionResult RawMaterialsView()
        {
            var items = RawMaterialService.GetAllRawMaterials();
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View(items);
        }

        public ActionResult CreateRawMaterialView()
        {

            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return PartialView("CreateRawMaterialView");
        }

        public ActionResult EditRawMaterial(int id)
        {

            var rawMaterial = RawMaterialService.GetRawMaterialById(id);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View(rawMaterial);
        }
        [HttpPost]
        public ActionResult CreateMeasurementType(string measurementType)
        {
            var items = RawMaterialService.GetAllRawMaterials();
            if (measurementType.IsEmpty() || measurementType.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("", "Navn må ikke være tomt.");

                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("RawMaterialsView", items);
            }

            string measurementTypeCapitalized = Helper.CapitalizeFirstLetter(measurementType);

            if (MeasurementTypeService.IsDuplicateName(measurementTypeCapitalized))
            {
                ModelState.AddModelError("", "Enhed med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("RawMaterialsView", items);
            }

            MeasurementTypeService.CreateMeasurementType(measurementTypeCapitalized);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            ModelState.Remove("measurementType");

            return View("RawMaterialsView", items);
        }
        [HttpPost]
        public ActionResult DeleteRawMaterial(int id)
        {
            // Hent råvaren ved ID
            var rawMaterial = RawMaterialService.GetRawMaterialById(id);
            if (rawMaterial != null)
            {
                // Slet råvaren via service
                RawMaterialService.DeleteRawMaterial(id);
            }

            // Omdiriger tilbage til Råvarer oversigt (hvis du har en RåvarerView)
            return RedirectToAction("RawMaterialsView");
        }
        [HttpPost]
        public ActionResult EditRawMaterial(int id, string name, string unit)
        {

            string nameCapitalized = Helper.CapitalizeFirstLetter(name);
            var existingRawMaterial = RawMaterialService.GetRawMaterialById(id);

            if (existingRawMaterial == null)
            {
                ModelState.AddModelError("", "Råvaren blev ikke fundet.");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View(RawMaterialService.GetRawMaterialById(id));
            }

            if (!existingRawMaterial.Name.Equals(nameCapitalized, StringComparison.OrdinalIgnoreCase)
                && RawMaterialService.IsDuplicateName(nameCapitalized))
            {
                ModelState.AddModelError("", "Råvare med samme navn eksisterer allerede.");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View(RawMaterialService.GetRawMaterialById(id));
            }

            RawMaterialDTO rawDTO = new RawMaterialDTO
            {
                Material_id = id,
                Name = nameCapitalized,
                MeasurementType = MeasurementTypeMapper.Map(MeasurementTypeService.GetMeasurementTypeByName(unit)),
            };

            RawMaterialService.UpdateRawMaterial(rawDTO);

            return RedirectToAction("RawMaterialsView");
        }
        public ActionResult ShowRawMaterial(int id)
        {
            var rawMaterial = RawMaterialService.GetRawMaterialById(id);

            rawMaterial.Stocks = rawMaterial.Stocks
                .OrderBy(stock => stock.ExpirationDate)
                .ToList();

            return View(rawMaterial);
        }
        [HttpPost]
        public ActionResult DeleteRawMaterialStock(int id, int rawMaterialId, double amount)
        {
            RawMaterialStockService.RemoveRawMaterialStock(id, amount);

            return RedirectToAction("ShowRawMaterial", new { id = rawMaterialId });
        }
        [ChildActionOnly]
        public ActionResult CreateMeasurementType()
        {
            return PartialView("CreateMeasurementType");
        }

        public ActionResult DeleteMeasurementType()
        {
            var measurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            Console.WriteLine(measurementTypes.Count);
            return View(measurementTypes);
        }
        [HttpPost]
        public ActionResult CreateRawMaterial(string name, string unit)
        {
            if (name == null || unit == null)
            {
                ModelState.AddModelError("", "Alle felter skal udfyldes.");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            string nameCapitalized = Helper.CapitalizeFirstLetter(name);

            if (RawMaterialService.IsDuplicateName(nameCapitalized))
            {
                ModelState.AddModelError("", "Råvare med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            RawMaterialService.CreateRawMaterial(nameCapitalized, MeasurementTypeMapper.Map(MeasurementTypeService.GetMeasurementTypeByName(unit)));

            // Redirect til råvareroversigten
            return RedirectToAction("RawMaterialsView");
        }
        [HttpPost]
        public ActionResult DeleteMeasurementType(string name)
        {
            var rawMats = RawMaterialService.GetAllRawMaterials();

            if (rawMats.Any(rm => rm.MeasurementType.Name.Equals(name)))
            {
                ModelState.AddModelError("", "Kan ikke slette en måleenhed, som bruges aktivt af en eller flere råvare(r)");
                return View(MeasurementTypeService.GetAllMeasurementTypes());

            }

            MeasurementTypeService.DeleteMeasurementType(name);

            return RedirectToAction("RawMaterialsView", rawMats);
        }
    }
}
