using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages;
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
        public ActionResult Index()
        {
            ViewBag.Message = "Your home page.";
            return View();
        }

        public ActionResult RåvarerView()
        {
            var items = RawMaterialService.GetAllRawMaterials();
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();

            Console.WriteLine(items);

            return View(items);
        }

        [HttpPost]
        public ActionResult StartProduction(string productionName, int plannedQuantity)
        {

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


        public ActionResult ProduktionView()
        {
           
            List<ProductProductionDTO> model = ProductProductionService.GetAllProductProductions();

            ViewBag.Message = "Your production page.";
            return View(model);
        }

         [HttpPost]
         public ActionResult RegisterSale(string productName, int quantitySold)
         {
             return RedirectToAction("ProduktView");
         } 

        [HttpPost]
        public ActionResult CompleteProduction(string productionId, int completedQuantity)
        {

            return RedirectToAction("ProduktView");
        }

        [HttpPost]
        public ActionResult CreateProduct(FormCollection formdata)
        {
            if (ModelState.IsValid)
            {
                var materialName = formdata["SelectedMaterials"];
                var materialAmountString = formdata["Amounts"];
                
                List<ProductRawMaterialNeeded> rawMaterialNeededList = new List<ProductRawMaterialNeeded>();
                int counter = 0;
                foreach (var material in materialName.Split(','))
                {
                    var rawMaterialDTO = RawMaterialService.GetRawMaterialByName(material)[0];

                    var rawMaterial = new RawMaterial
                    {
                        Material_id = rawMaterialDTO.Material_id,
                        Name = rawMaterialDTO.Name,
                        MeasurementType = rawMaterialDTO.MeasurementType,
                        Stocks = new List<RawMaterialStock> { new RawMaterialStock { Amount = Convert.ToDouble(materialAmountString.Split(',')[counter]) } }
                    };

                    var rawMaterialNeeded = new ProductRawMaterialNeeded
                    {
                        RawMaterial = rawMaterial,
                        Quantity = Double.Parse(materialAmountString.Split(',')[counter]),
                    };
                    rawMaterialNeededList.Add(rawMaterialNeeded);
                    counter++;
                }

                
                var product = new ProductDTO
                {
                    Name = formdata["name"],
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    ProductRawMaterialNeeded = new List<ProductRawMaterialNeeded>(),
                    EstimatedProductionTime = TimeSpan.FromHours(int.Parse(formdata["EstimatedProductionTime"])),
                    AmountInStock = int.Parse(formdata["amount"])
                };
                foreach (var rawMaterialNeeded in rawMaterialNeededList)
                {
                    product.ProductRawMaterialNeeded.Add(rawMaterialNeeded);
                }
                ProductRepository.AddProduct(product);

                return RedirectToAction("ProduktView");
            }

            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View("CreateProductView");
        }


        [HttpPost]
        public ActionResult RecordPurchase(int materialId, double amount, DateTime? expirationDate)
        {
            var rawMaterial = RawMaterialService.GetRawMaterialById(materialId);

            rawMaterial.AddStock(amount, expirationDate);

            RawMaterialService.AddStockToRawMaterial(rawMaterial);

            return RedirectToAction("Index");
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

        public ActionResult CompleteProductProductionView(string name)
        {
            var productProduction = ProductProductionService.GetProductProductionByName(name);

            if (productProduction == null)
            {
                ModelState.AddModelError("", "Produktion findes ikke");
            }

            return View(productProduction);
        }

        [HttpPost]
        public ActionResult CompleteProductProduction(string name)
        {
            var productProduction = ProductProductionService.GetProductProductionByName(name);

            if (productProduction == null)
            {
                ModelState.AddModelError("", "Produktion findes ikke");
            }

            productProduction.Status = Status.Completed;
            ProductProductionService.UpdateProductProduction(productProduction);

            return RedirectToAction("ProduktionView");
        }

        public ActionResult DeleteProductProduction(string name)
        {
            ProductProductionService.DeleteProductProduction(ProductProductionService.GetProductProductionByName(name));
            return RedirectToAction("ProduktionView");
        }


        [HttpPost]
        public ActionResult EditProductProduction(int id, string name, string status)
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

            existingProductProduction.ProjectName = nameCapitalized;
            existingProductProduction.Status = (Status)Enum.Parse(typeof(Status), status);

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

            return RedirectToAction("Index");
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteRawMaterial(int id)
        {
            var rawMaterial = RawMaterialService.GetRawMaterialById(id);
            if (rawMaterial != null)
            {
                RawMaterialService.DeleteRawMaterial(id);
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult CreateMeasurementType(string measurementType)
        {
            var items = RawMaterialService.GetAllRawMaterials();
            if (measurementType.IsEmpty() || measurementType.IsNullOrWhiteSpace())
            {
                ModelState.AddModelError("", "Navn må ikke være tomt.");

                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("Index", items);
            }

            string measurementTypeCapitalized = Helper.CapitalizeFirstLetter(measurementType);

            if (MeasurementTypeService.IsDuplicateName(measurementTypeCapitalized))
            {
                ModelState.AddModelError("", "Enhed med samme navn eksisterer allerede");
                ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
                return View("Index", items);
            }

            MeasurementTypeService.CreateMeasurementType(measurementTypeCapitalized);
            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            ModelState.Remove("measurementType");

            return View("Index", items);
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

            return RedirectToAction("Index", rawMats);
        }
    }
}