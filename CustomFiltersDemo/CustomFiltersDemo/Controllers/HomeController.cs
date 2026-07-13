using CustomFiltersDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CustomFiltersDemo.Filters;

namespace CustomFiltersDemo.Controllers
{
    // [MyCustomActionFilter]
    //[MyAsyncCustomActionFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        // [MyCustomActionFilter(Order =2)]
        // [MyAsyncCustomActionFilter(Order =1)]

        // [MyNewCustomActionFilter]
        [ServiceFilter(typeof(MyNewCustomActionFilter))]
        public IActionResult Index()
        {
            int a, b, c;
            a = 10;
            b = 20;
            c = a + b;
            c = a - b;
            c = a * b;
            c = a / b;
            return View();
        }
        // [MyCustomActionFilter]
       // [MyAsyncCustomActionFilter]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
