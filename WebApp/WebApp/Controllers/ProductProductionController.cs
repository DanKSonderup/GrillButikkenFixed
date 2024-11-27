using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DTO;
using WebApp.DTO.Mappers;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class ProductProductionController : Controller
    {
        // GET: ProductProduction
        public ActionResult Index()
        {
            List<ProductProductionDTO> model = ProductProductionService.GetAllProductProductions();

            ViewBag.Message = "Your production page.";
            return View(model);
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
    }
}