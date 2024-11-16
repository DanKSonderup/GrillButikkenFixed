using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApp.BLL;
using WebApp.DataAccess.Repositories;
using WebApp.DTO;
using WebApp.Models;
using WebApp.Models.Frontend;
using Unit = WebApp.Models.Frontend.Unit;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ProductionFactory grillSpydFactory = new ProductionFactory();
        public ActionResult Index()
        {
            BilBLL bll = new BilBLL();
            BilDTO bildto = bll.getBil(1);
            ViewBag.Message = "Your home page.";
            return View();
        }

        public ActionResult RåvarerView()
        {
            List<RawMaterialDTO> råvarer = RawMaterialRepository.GetRawMaterials();

            return View(råvarer);
        }

        [HttpPost]
        public ActionResult StartProduction(string productionName, int plannedQuantity)
        {
            List<RawMaterials> items = GetItems();

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
            List<ProductDTO> products = ProductRepository.GetProducts();

            ViewBag.Message = "Your products page.";
            return View(products);
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
    }
}