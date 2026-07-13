using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkingWithControllersDemo.Models;

namespace WorkingWithControllersDemo.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Greetings = "Hello World From MVC Core";
            ViewBag.Company = "Udemy";
            ViewBag.Country = "India";

            Authors obj = new Authors();
            obj.Name = "Connors McGregor";
            obj.Country = "USA";


            return View(obj); //Passing Model data to view
        }


        public IActionResult QueryStringDemo(string Message= "Hello World From MVC Core")
        {
            ViewBag.Greetings = Message;
            return View();
        }


        [HttpGet]
        public IActionResult GotoURL(string url="http://www.google.com")
        {
            return (Redirect(url));
        }

        [HttpPost]
        public IActionResult GotoURL(IFormCollection ifc)
        {
            string url = ifc["url"];

            if (url == string.Empty)
            {
                return (Redirect("http://www.google.com"));
            }
            else
            {
                return (Redirect(url));
            }
        }
    }
}
