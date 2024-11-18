using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Xml.Linq;
using WebApp.BLL;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            BilBLL bll = new BilBLL();
            BilDTO bildto = bll.getBil(1);
            ViewBag.Message = "Your home page.";
            return View();
        }

        public ActionResult RåvarerView()
        {
            IEnumerable<RawMaterialDTO> rawMaterials = RawMaterialService.GetAllRawMaterials();

            ViewBag.Message = "RawrMaterials";
            return View(rawMaterials);
        }

        public ActionResult ProduktView()
        {

            IEnumerable<ProductDTO> products = ProductService.GetAllProducts();
            ViewBag.Message = "Your products page.";
            return View(products);
        }

        public ActionResult ProduktionView()
        {
            IEnumerable<ProductProductionDTO> productProductions = ProductProductionService.GetAllProductProductions();
            return View(productProductions);
        }


        [HttpPost]
         public ActionResult RegisterSale(string productName, int quantitySold)
         {
             // ProductService.UpdateInventory(productName, -quantitySold);
             return RedirectToAction("ProduktView");
         }

        [HttpPost]
        public ActionResult CreateProduction(string projectName, int plannedQuantity, Product product, DateTime deadline, Status status)
        {
            if (projectName == null || plannedQuantity == 0 || product == null)
            {
                ModelState.AddModelError("", "Alle felter skal udfyldes.");
                return View("CreateProductProductionView");
            }

            ProductProductionService.CreateProductProduction(projectName, product, plannedQuantity, DateTime.Now, deadline, status);

            return View("ProduktionView");
        }

        [HttpPost]
        public ActionResult CompleteProduction(int Id, int completedQuantity)
        {
            ProductProductionDTO production = ProductProductionService.GetAllProductProductions()
                                            .FirstOrDefault(item => item.ProjectId == Id);
            Product product = production.Product;

            production.Status = Status.Completed;
            production.QuantityToProduce = completedQuantity;
            product.AmountInStock += completedQuantity;
            ProductProductionService.UpdateProductProduction(production);
            ProductProductionService.UpdateStatusForProductProduction(Status.Completed, production);

            return View("ProduktionView");
        }

        [HttpPost]
        public ActionResult DeleteProduction(int Id)
        {
            ProductProductionDTO production = ProductProductionService.GetAllProductProductions()
                                            .FirstOrDefault(item => item.ProjectId == Id);
            ProductProductionService.DeleteProductProduction(production);

            return RedirectToAction("ProduktionView");
        }

        [HttpPost]
        public ActionResult UpdateProduction(int Id)
        {
            ProductProductionDTO production = ProductProductionService.GetAllProductProductions()
                                            .FirstOrDefault(item => item.ProjectId == Id);

            ProductProductionService.UpdateProductProduction(production);

            return View("ProduktionView");
        }

        /*
        private ProductProduction GetProductionById(string productionId)
        {
            // Implement logic to retrieve production by ID
            return new ProductProduction("Dummy", DateTime.Now, 0, new List<RawMaterials>());
        } */



        public ActionResult CreateRawMaterialView()
        {
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View();
        }

        [ChildActionOnly]
        public ActionResult CreateMeasurementType()
        {
            return PartialView("CreateMeasurementType");
        }

        [HttpPost]
        public ActionResult CreateRawMaterial(string name, string unit, double amount)
        {
            if (name == null || unit == null || amount < 0)
            {
                ModelState.AddModelError("", "Alle felter skal udfyldes.");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("CreateRawMaterialView");
            }

            var test = MeasurementTypeMapper.Map(MeasurementTypeService.GetMeasurementTypeByName(unit));
            Console.WriteLine(test);

            RawMaterialService.CreateRawMaterial(name, MeasurementTypeMapper.Map(MeasurementTypeService.GetMeasurementTypeByName(unit)), amount);

            // Redirect til råvareroversigten
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

            MeasurementTypeService.CreateMeasurementType(measurementType);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            ModelState.Remove("measurementType");

            return View("CreateRawMaterialView");
        }

        public ActionResult EditRawMaterial()
        {

            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View();
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
    }
}