using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using WebApp.BLL;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //private ProductionFactory grillSpydFactory = new ProductionFactory();
        public ActionResult Index()
        {
            BilBLL bll = new BilBLL();
            BilDTO bildto = bll.getBil(1);
            ViewBag.Message = "Your home page.";
            return View();
        }

        public ActionResult RåvarerView()
        {

            List<RawMaterialDTO> items = new List<RawMaterialDTO>
            {
                new RawMaterialDTO("Stål", new MeasurementType("kg"), 50),
                new RawMaterialDTO("Træ", new MeasurementType("kg"), 24)
            };

            items = RawMaterialService.GetAllRawMaterials();

            return View(items);
        }

        [HttpPost]
        public ActionResult StartProduction(string productionName, int plannedQuantity)
        {
            //List<RawMaterialDTO> items = GetItems();

            //// Assuming grillSpydFactory is defined elsewhere in your code
            //Production production = grillSpydFactory?.CreateProduction(productionName, plannedQuantity, items);

            //production?.StartProduction();
            return RedirectToAction("TestView");
        }


        private List<RawMaterialDTO> GetItems()
        {

            return new List<RawMaterialDTO>
            {
                new RawMaterialDTO("Stål", new MeasurementType("kg"), 50),
                new RawMaterialDTO("Træ", new MeasurementType("kg"), 24)
            };
        }

        public ActionResult IndstillingerView()
        {
            ViewBag.Message = "Your settings page.";
            return View();
        }

        public ActionResult StatistikView()
        {
            ViewBag.Message = "Your statistics page.";
            return View();
        }

        public ActionResult ProduktView()
        {
            List<ProductDTO> products = ProductRepository.GetProducts();

            ViewBag.Message = "Your products page.";
            return View(products);
        }

        public ActionResult ProduktionView()
        {
            /*
            List<Production> produktioner = new List<Production>
            {
                new Production("Grillspyd", DateTime.Now, 10, new List<RawMaterialDTO>
                {
                    new RawMaterialDTO("Stål", Unit.kg, 20) { Category = "Metal" },
                    new RawMaterialDTO("Træ", Unit.pcs, 20) { Category = "Material" }
                }),
                    new Production("Slaver", DateTime.Now, 10, new List<RawMaterialDTO>
                    {
                    new RawMaterialDTO("Pisk", Unit.pcs, 2) { Category = "Metal" },
                    new RawMaterialDTO("Bomuld", Unit.kg, 200) { Category = "Material" }
                })
            }; 
            var model = new InventoryAndProductionOverview
            {
                Productions = produktioner
            }; */

            List<ProductProductionDTO> model = new List<ProductProductionDTO>();

            ViewBag.Message = "Your production page.";
            return View(model);
        }



        public ActionResult CreateRawMaterialView()
        {

            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View();
        }

        public ActionResult EditRawMaterial(int id)
        {

            var rawMaterial = RawMaterialService.GetRawMaterialById(id);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View(rawMaterial);
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
        public ActionResult CreateRawMaterial(string name, string unit, string amount, DateTime? expirationDate = null)
        {
            if (name == null || unit == null || amount == null)
            {
                ModelState.AddModelError("", "Alle felter skal udfyldes.");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            string nameCapitalized = Helper.CapitalizeFirstLetter(name);
            double amountParsed = Double.Parse(amount);


            if (RawMaterialService.IsDuplicateName(nameCapitalized))
            {
                ModelState.AddModelError("", "Råvare med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            RawMaterialService.CreateRawMaterial(nameCapitalized, MeasurementTypeMapper.Map(MeasurementTypeService.GetMeasurementTypeByName(unit)), amountParsed, expirationDate);

            // Redirect til råvareroversigten
            return RedirectToAction("RåvarerView");
        }

        [HttpPost]
        public ActionResult EditRawMaterial(int id, string name, string unit, string amount, DateTime? expirationDate = null)
        {

            string nameCapitalized = Helper.CapitalizeFirstLetter(name);
            double amountParsed = Double.Parse(amount);

            if (RawMaterialService.IsDuplicateName(nameCapitalized))
            {
                ModelState.AddModelError("", "Råvare med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            RawMaterialDTO rawDTO = new RawMaterialDTO
            {
                Material_id = id,
                Name = nameCapitalized,
                MeasurementType = MeasurementTypeMapper.Map(MeasurementTypeService.GetMeasurementTypeByName(unit)),
                Stocks = new List<RawMaterialStock>
                {
                    new RawMaterialStock(id, amountParsed, expirationDate)
                }
            };

            RawMaterialService.UpdateRawMaterial(rawDTO);

            return RedirectToAction("RåvarerView");
        }

        [HttpPost]
        public ActionResult CreateMeasurementType(string measurementType)
        {
            if (measurementType.IsEmpty() || measurementType.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("", "Navn må ikke være tomt.");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            string measurementTypeCapitalized = Helper.CapitalizeFirstLetter(measurementType);

            if (MeasurementTypeService.IsDuplicateName(measurementTypeCapitalized))
            {
                ModelState.AddModelError("", "Enhed med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            MeasurementTypeService.CreateMeasurementType(measurementTypeCapitalized);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            ModelState.Remove("measurementType");

            return View("CreateRawMaterialView");
        }


        [HttpPost]
        public ActionResult DeleteMeasurementType(string name)
        {
            MeasurementTypeService.DeleteMeasurementType(name);

            var items = RawMaterialService.GetAllRawMaterials();
            return View("RåvarerView", items);
        }
    }
}