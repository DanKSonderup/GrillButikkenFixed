using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class ProductProductionController : Controller
    {
        private ProductProductionService _productionService;

        public ProductProductionController(ProductProductionService productionService)
        {
            _productionService = productionService;
        }

        // GET: ProductProduction

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProductProduction(FormCollection formData)
        {
            /*
            // Add new project to the database
            _productionService.CreateProductProduction();

            // Retrieve the updated list of projects
            var updatedProjects = _productionService.GetAllProductProductions();

            // Return the updated list to the view */
            return View("Index");
        }

        [HttpPut]
        public ActionResult UpdateProductProduction(FormCollection formData)
        {
            // _productionService.UpdateProductionProjectAsync(null);

            // View should return latest list of all ProductProductions
            return View("Index");
        }

    }
}