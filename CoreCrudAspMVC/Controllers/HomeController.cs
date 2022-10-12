using CoreCrudAspMVC.Models;
using DataAccessLayerOne;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreCrudAspMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        

        {
            List<RaniEmployee> raniEmployees = new List<RaniEmployee>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await client.GetAsync("api/raniEmployees");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                raniEmployees = JsonConvert.DeserializeObject<List<RaniEmployee>>(results);
            }

             return View(raniEmployees);
        }

        public async Task<IActionResult> Detail(int id)
        {
            RaniEmployee raniEmployee = await GetRaniEmployeeByID(id);

            return View(raniEmployee);
        }

        private static async Task<RaniEmployee> GetRaniEmployeeByID(int id)
        {
            RaniEmployee raniEmployee = new RaniEmployee();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await client.GetAsync($"api/raniEmployees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                raniEmployee = JsonConvert.DeserializeObject<RaniEmployee>(results);
            }

            return raniEmployee;
        }

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RaniEmployee raniEmployee)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            var response = await client.PostAsJsonAsync("api/raniEmployees", raniEmployee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await client.DeleteAsync($"api/raniEmployee/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public async Task< IActionResult> Edit(int id)
        {
            RaniEmployee raniEmployee = await GetRaniEmployeeByID(id);
            return View(raniEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RaniEmployee raniEmployee)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001/");
            var response = await client.PutAsJsonAsync($"api/raniEmployees/{ raniEmployee.Id}", raniEmployee);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


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
