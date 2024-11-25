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
            var items = RawMaterialService.GetAllRawMaterials();
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes(); // Used when creating a raw material

            Console.WriteLine(items);

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
            List<ProductRawMaterialNeededDTO> productRawMaterialNeededDTOs = ProductRawMaterialNeededRepository.GetProductRawMaterialNeededFromProduct(products[0]);

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

        public ActionResult CreateProductView()
        {
            ViewBag.Products = ProductRepository.GetProducts();
            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(string name, int EstimatedProductionTime, int amount)
        {
            if (ModelState.IsValid)
            {
                var product = new ProductDTO
                {
                    Id = 0,
                    Name = name,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    RawMaterialNeeded = new Dictionary<RawMaterial, Double>(),
                    EstimatedProductionTime = TimeSpan.FromHours(EstimatedProductionTime),
                    AmountInStock = +amount
                };
                ProductRepository.AddProduct(product);

                // Redirect to a confirmation page or another action
                return RedirectToAction("ProduktView");
            }

            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View("CreateProductView");
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



        public ActionResult ShowRawMaterial(int id)
        {
            var rawMaterial = RawMaterialService.GetRawMaterialById(id);

            rawMaterial.Stocks = rawMaterial.Stocks
                .OrderBy(stock => stock.ExpirationDate)
                .ToList();

            return View(rawMaterial);
        }

        [HttpPost]
        public ActionResult RecordPurchase(int materialId, double amount, DateTime? expirationDate)
        {
            var rawMaterial = RawMaterialService.GetRawMaterialById(materialId);

            rawMaterial.AddStock(amount, expirationDate);

            RawMaterialService.AddStockToRawMaterial(rawMaterial);

            return RedirectToAction("RåvarerView");
        }

        [HttpPost]
        public ActionResult DeleteRawMaterialStock(int id, int rawMaterialId, double amount)
        {
            RawMaterialStockService.RemoveRawMaterialStock(id, amount);
            
            return RedirectToAction("ShowRawMaterial", new { id = rawMaterialId });
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

        public ActionResult DeleteMeasurementType()
        {
            var measurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            Console.WriteLine(measurementTypes.Count);
            return View(measurementTypes);
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
        public ActionResult EditProductProduction(int id, string name, int amount, string status, DateTime endDate)
        {
            List<ProductProductionDTO> model = ProductProductionService.GetAllProductProductions();

            string nameCapitalized = Helper.CapitalizeFirstLetter(name);
            var existingProductProduction = ProductProductionService.GetProductProductionById(id);

            if (existingProductProduction == null)
            {
                ModelState.AddModelError("", "Produktionen blev ikke fundet.");
                ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();
                return View(model);
            }

            if (!existingProductProduction.ProjectName.Equals(nameCapitalized, StringComparison.OrdinalIgnoreCase)
                && ProductProductionService.IsDuplicateName(nameCapitalized))
            {
                ModelState.AddModelError("", "Produktion med samme navn eksisterer allerede.");
                ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();
                return View("ProduktionView", model);
            }



            existingProductProduction.QuantityToProduce = amount;
            existingProductProduction.ProjectName = nameCapitalized;
            existingProductProduction.Status = (Status)Enum.Parse(typeof(Status), status);
            existingProductProduction.Deadline = endDate;

            ProductProductionService.UpdateProductProduction(existingProductProduction);



            return RedirectToAction("ProduktionView", model);
        }

        public ActionResult EditProductProduction(string name)
        {
            var productProduction = ProductProductionService.GetProductProductionByName(name);
            ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();

            return View(productProduction);
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
            return RedirectToAction("RåvarerView");
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

            if(!existingRawMaterial.Name.Equals(nameCapitalized, StringComparison.OrdinalIgnoreCase)
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

            return RedirectToAction("RåvarerView");
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
            return RedirectToAction("RåvarerView");
        }



        [HttpPost]
        public ActionResult CreateMeasurementType(string measurementType)
        {
            var items = RawMaterialService.GetAllRawMaterials();
            if (measurementType.IsEmpty() || measurementType.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("", "Navn må ikke være tomt.");

                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("RåvarerView", items);
            }

            string measurementTypeCapitalized = Helper.CapitalizeFirstLetter(measurementType);

            if (MeasurementTypeService.IsDuplicateName(measurementTypeCapitalized))
            {
                ModelState.AddModelError("", "Enhed med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("RåvarerView", items);
            }

            MeasurementTypeService.CreateMeasurementType(measurementTypeCapitalized);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            ModelState.Remove("measurementType");

            return View("RåvarerView", items);
        }


        [HttpPost]
        public ActionResult DeleteMeasurementType(string name)
        {
            var rawMats = RawMaterialService.GetAllRawMaterials();

            if(rawMats.Any(rm => rm.MeasurementType.Name.Equals(name)))
            {
                ModelState.AddModelError("", "Kan ikke slette en måleenhed, som bruges aktivt af en eller flere råvare(r)");
                return View(MeasurementTypeService.GetAllMeasurementTypes());

            }

            MeasurementTypeService.DeleteMeasurementType(name);

            return RedirectToAction("RåvarerView", rawMats);
        }
    }
}