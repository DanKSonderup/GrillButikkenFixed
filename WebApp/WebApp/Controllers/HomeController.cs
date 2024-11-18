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
           

            List<ProductProductionDTO> model = ProductProductionService.GetAllProductProductions();

            ViewBag.Message = "Your production page.";
            return View(model);
        }

         [HttpPost]
         public ActionResult RegisterSale(string productName, int quantitySold)
         {
             // ProductService.UpdateInventory(productName, -quantitySold);
             return RedirectToAction("ProduktView");
         } 

        [HttpPost]
        public ActionResult CompleteProduction(string productionId, int completedQuantity)
        {
            // ProductService production = GetProductionById(productionId);
            // if (production != null)
            {
               // production.CompleteProduction(completedQuantity);
                //ProductService.UpdateInventory(productProduction.ProductName, completedQuantity);
            }
            return RedirectToAction("ProduktView");
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

        public ActionResult SearchForProductsContaining(string name)
        {
            ProductService productService = new ProductService();
            var products = productService.GetAllProductsWithNameContaining(name);

            return View("ProduktView", products);
        }

        [ChildActionOnly]
        public ActionResult CreateMeasurementType()
        {
            return PartialView("CreateMeasurementType");
        }

        public ActionResult CreateProductProductionView()
        {
            ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();
            ViewBag.Products = ProductService.GetAllProducts();

            return View();
        }

        [HttpPost]
        public ActionResult CreateProductProduction(string name, string product, DateTime startDato, DateTime endDato, string status, int amount)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(product) || string.IsNullOrEmpty(status) || amount < 0)
            {
                ModelState.AddModelError("", "Alle felter skal udfyldes.");
                ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();
                ViewBag.Products = ProductService.GetAllProducts();
                return View("CreateProductProduction");
            }


            ProductProductionService.CreateProductProduction(name, ProductMapper.Map(ProductService.GetProductByName(product)), amount, startDato, endDato, (Status)Enum.Parse(typeof(Status), status));
            return RedirectToAction("ProduktionView");
        }

        [HttpGet]
        public ActionResult DeleteProductProduction(string name)
        {
            ProductProductionService.DeleteProductProduction(ProductProductionService.GetProductProductionByName(name));
            return RedirectToAction("ProduktionView");
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
    }
}