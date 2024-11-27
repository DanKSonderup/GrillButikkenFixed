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
    public class ProductsController : Controller
    {
        public ActionResult ProductView()
        {
            var products = ProductRepository.GetProducts();
            return View(products);
        }

        public ActionResult CreateProductView()
        {
            ViewBag.Products = ProductRepository.GetProducts();
            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterSale(string productName, int quantitySold)
        {
            // ProductService.UpdateInventory(productName, -quantitySold);
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
                return RedirectToAction("ProductView");
            }

            ViewBag.RawMaterials = RawMaterialService.GetAllRawMaterials();
            return View("CreateProductView");
        }
    }
}
