using System;
using System.Linq;
using System.Web.Mvc;
using WebApp.DTO.Mappers;
using WebApp.Helpers;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{

    public class ProductProductionsController : Controller
    {
        public ActionResult ProductionView()
        {
            var productions = ProductProductionService.GetAllProductProductions();
            ViewBag.Message = "Your production page.";
            return View(productions);
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
                return View("CreateProductProductionView");
            }

            var productEntity = ProductService.GetProductByName(product);
            if (productEntity == null)
            {
                ModelState.AddModelError("", "Produktet blev ikke fundet.");
                ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();
                return View("CreateProductProductionView");
            }

            ProductProductionService.CreateProductProduction(
                name,
                ProductMapper.Map(productEntity),
                amount,
                startDato,
                endDato,
                (Status)Enum.Parse(typeof(Status), status)
            );

            return RedirectToAction("ProductionView");
        }

        public ActionResult CompleteProductProductionView(string name)
        {
            var production = ProductProductionService.GetProductProductionByName(name);

            if (production == null)
            {
                ModelState.AddModelError("", "Produktion findes ikke.");
                return RedirectToAction("ProductionView");
            }

            return View(production);
        }

        [HttpPost]
        public ActionResult CompleteProductProduction(string name)
        {
            var production = ProductProductionService.GetProductProductionByName(name);

            if (production == null)
            {
                ModelState.AddModelError("", "Produktion findes ikke.");
                return RedirectToAction("ProductionView");
            }

            production.Status = Status.Completed;
            ProductProductionService.UpdateProductProduction(production);

            return RedirectToAction("ProductionView");
        }

        [HttpPost]
        public ActionResult EditProductProduction(int id, string name, string status)
        {
            var existingProduction = ProductProductionService.GetProductProductionById(id);
            if (existingProduction == null)
            {
                ModelState.AddModelError("", "Produktionen blev ikke fundet.");
                return RedirectToAction("ProductionView");
            }

            var nameCapitalized = Helper.CapitalizeFirstLetter(name);
            if (!existingProduction.ProjectName.Equals(nameCapitalized, StringComparison.OrdinalIgnoreCase) &&
                ProductProductionService.IsDuplicateName(nameCapitalized))
            {
                ModelState.AddModelError("", "Produktion med samme navn eksisterer allerede.");
                return RedirectToAction("ProductionView");
            }

            existingProduction.ProjectName = nameCapitalized;
            existingProduction.Status = (Status)Enum.Parse(typeof(Status), status);
            ProductProductionService.UpdateProductProduction(existingProduction);

            return RedirectToAction("ProductionView");
        }

        public ActionResult EditProductProduction(string name)
        {
            var production = ProductProductionService.GetProductProductionByName(name);
            ViewBag.StatusType = Enum.GetValues(typeof(Status)).Cast<Status>();

            return View(production);
        }

        public ActionResult DeleteProductProduction(string name)
        {
            var production = ProductProductionService.GetProductProductionByName(name);

            if (production == null)
            {
                ModelState.AddModelError("", "Produktion findes ikke.");
                return RedirectToAction("ProductionView");
            }

            ProductProductionService.DeleteProductProduction(production);
            return RedirectToAction("ProductionView");
        }
    }
}
