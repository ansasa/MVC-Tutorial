using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewComponentsDemo.Models;

namespace ViewComponentsDemo.Controllers
{
    [ViewComponent(Name = "ProductsControllerHybrid")]
    public class ProductsController : Controller
    {

        private ProductsData data;

        public ProductsController(ProductsData pdata)
        {
            data = pdata;
        }
        public IActionResult Index()
        {
            return View(data.Products);
        }

        public IViewComponentResult Invoke()
        {
            return new ViewViewComponentResult()
            {
                ViewData = new ViewDataDictionary<ProductViewModel>(
            ViewData,
            new ProductViewModel
            {
                ProductsCount = data.Products.Count(),
                StockWorth = data.Products.Sum(c => c.Cost)
            })
            };
        }
    }
}
