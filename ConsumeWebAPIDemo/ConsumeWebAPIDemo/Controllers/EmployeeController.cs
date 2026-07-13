using ConsumeWebAPIDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsumeWebAPIDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient client = null;
        private string employeesApiUrl = "";

        public EmployeeController(HttpClient client, IConfiguration config)
        {
            this.client = client;
            employeesApiUrl = config.GetValue<string>("AppSettings:EmployeesApiUrl");
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(employeesApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<Employee> data = JsonSerializer.Deserialize<List<Employee>>(stringData, options);

            return View(data);
        }


        public async Task<IActionResult> Details(int id)
        {

            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {

            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);
                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(employeesApiUrl, contentData);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Employee inserted successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling Web API!";
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int id)
        {

            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee model)
        {

            if (ModelState.IsValid)
            {
                string stringData = JsonSerializer.Serialize(model);

                var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");


                HttpResponseMessage response = await client.PutAsync
            ($"{employeesApiUrl}/{model.EmployeeID}", contentData);
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Employee updated successfully!";
                }
                else
                {
                    ViewBag.Message = "Error while calling Web API!";
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{employeesApiUrl}/{id}");
            string stringData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee model = JsonSerializer.Deserialize<Employee>(stringData, options);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{employeesApiUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Employee deleted successfully!";
            }
            else
            {
                ViewBag.Message = "Error while calling Web API!";
            }
            return RedirectToAction("Index");
        }
    }
}
