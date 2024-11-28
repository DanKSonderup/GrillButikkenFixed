using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<ProductDTO> products = ProductRepository.GetProducts();
            // List<ProductRawMaterialNeededDTO> productRawMaterialNeededDTOs = ProductRawMaterialNeededRepository.GetProductRawMaterialNeededFromProduct(products[0]);

            ViewBag.Message = "Your products page.";
            return View(products);
        }

        [HttpGet]
        public ActionResult CreateProductView()
        {
            ViewBag.Products = ProductRepository.GetProducts();
            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View();
        }

        // Lav et Product
        [HttpPost]
        public ActionResult CreateProduct(FormCollection formdata)
        {
            if (ModelState.IsValid)
            {
                var materialName = formdata["SelectedMaterials"]; // Få værdien fra inputfeltet med name "MaterialName"
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

                return RedirectToAction("Index");
            }

            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View("CreateProductView");
        }

        [HttpPost]
        public ActionResult RegisterSale(string productName, int quantitySold)
        {
            return RedirectToAction("Index");
        }

        public ActionResult SearchForProductsContaining(string name)
        {
            ProductService productService = new ProductService();
            var products = productService.GetAllProductsWithNameContaining(name);

            return View("Index", products);
        }
    }
}