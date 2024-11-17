using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.BLL;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //private ProductionFactory grillSpydFactory = new ProductionFactory();
        public ActionResult Index()
        {
            // BilBLL bll = new BilBLL();
            // BilDTO bildto = bll.getBil(1);
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

            //List<Production> productions = new List<Production>
            //{

            //};

            var model = items;

            return View(model);
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
            // Dummydata her
            ViewBag.Message = "Your products page.";
            return View();
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

            var model = new List<ProductProductionDTO>();

            ViewBag.Message = "Your production page.";
            return View(model);
        }
    }
}