using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCCoreWebApp.Controllers
{
    public class FirstMVCIntroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public string Index2()
        {
            return ("Hello World From Index2");
        }

        public ViewResult Index3()
        {
            return View("Index33");
        }

        //Passing data from controller to view
        public ViewResult Index4()
        {
            int hour = DateTime.Now.Hour;
            string viewModel = hour < 12 ? "Good Morning" : "Good Afternoon";
            //return View(viewModel); This line throws an error as it treats viewModel variable as ModelName
            return View("Index4", viewModel);

        }

    }
}
