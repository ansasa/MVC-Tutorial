using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WorkingWithControllersDemo.Controllers
{
    public class ShowURLElementsController : Controller
    {
        public IActionResult Index()
        {

            var controller = RouteData.Values["controller"];
            var action = RouteData.Values["action"];
            var id = RouteData.Values["id"];

            var message = string.Format("{0}::{1}::{2}", controller, action, id);
            ViewBag.Message = message;



            return View();

            
        }
    }
}
