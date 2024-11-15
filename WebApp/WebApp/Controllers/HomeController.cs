using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.BLL;
using WebApp.DTO;
using WebApp.Models;
using WebApp.Models.Frontend;
using WebApp.Service;
using Unit = WebApp.Models.Frontend.Unit;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ProductionFactory grillSpydFactory = new ProductionFactory();
        public ActionResult Index()
        {
            // BilBLL bll = new BilBLL();
            // BilDTO bildto = bll.getBil(1);
            ViewBag.Message = "Your home page.";
            return View();
        }

        public ActionResult RåvarerView()
        {
            List<RawMaterials> items = new List<RawMaterials>
            {
                new RawMaterials("Stål", Unit.kg, 20) { Category = "Metal" },
                new RawMaterials("Træ", Unit.pcs, 20) { Category = "Material" }
            };

            List<Production> productions = new List<Production>
            {
                new Production("Grill Spyd", DateTime.Now, 10, items)
            };

            var model = new InventoryAndProductionOverview
            {
                RawMaterials = items,
                Productions = productions
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult StartProduction(string productionName, int plannedQuantity)
        {
            List<RawMaterials> items = GetItems();

            // Assuming grillSpydFactory is defined elsewhere in your code
            Production production = grillSpydFactory?.CreateProduction(productionName, plannedQuantity, items);

            production?.StartProduction();
            return RedirectToAction("TestView");
        }


        private List<RawMaterials> GetItems()
        {
            
            return new List<RawMaterials>
            {
                new RawMaterials("Steel", Unit.kg, 20) { Category = "Metal" },
                new RawMaterials("Wood", Unit.pcs, 20) { Category = "Material" }
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
            // Dummydata her
            ViewBag.Message = "Your products page.";
            return View();
        }

        public ActionResult ProduktionView()
        {
            List<Production> produktioner = new List<Production>
            {
                new Production("Grillspyd", DateTime.Now, 10, new List<RawMaterials>
                {
                    new RawMaterials("Stål", Unit.kg, 20) { Category = "Metal" },
                    new RawMaterials("Træ", Unit.pcs, 20) { Category = "Material" }
                }),
                    new Production("Slaver", DateTime.Now, 10, new List<RawMaterials>
                    {
                    new RawMaterials("Pisk", Unit.pcs, 2) { Category = "Metal" },
                    new RawMaterials("Bomuld", Unit.kg, 200) { Category = "Material" }
                })
            };
            var model = new InventoryAndProductionOverview
            {
                Productions = produktioner
            };

            ViewBag.Message = "Your production page.";
            return View(model);
        }

        public ActionResult CreateRawMaterialView()
        {

            ViewBag.MeasurementTypes = MeasurementTypeService.GetAllMeasurementTypes();
            return View();
        }

        // POST: CreateRawMaterial
        [HttpPost]
        public ActionResult CreateRawMaterial(string name, MeasurementType measurementType, double measurementValue)
        {
            // Simuler en service, der opretter råmaterialet
            RawMaterialService service = new RawMaterialService();
            service.CreateRawMaterial(name, measurementType, measurementValue);

            // Redirect til råvareroversigten
            return RedirectToAction("RåvarerView");
        }

        [HttpPost]
        public ActionResult CreateMeasurementType(string name)
        {
            MeasurementTypeService.CreateMeasurementType(name);

            return RedirectToAction("CreateRawMaterialView");
        }
    }
}