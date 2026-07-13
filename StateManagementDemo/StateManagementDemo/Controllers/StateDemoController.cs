using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StateManagementDemo.Controllers
{
    public class StateDemoController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Ecode"] = 101;
            ViewData["Ename"] = "Connors";

            ViewBag.Message = "This is coming from ViewBag.Message";

            if (TempData["Country"] == null)
            {
                TempData.Add("Country", "India");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection ifc)
        {
            return View();
        }
    }
}
