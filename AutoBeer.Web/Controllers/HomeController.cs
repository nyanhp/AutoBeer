using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoBeer.Models;
using AutoBeer.Data.Services;

namespace AutoBeer.Controllers
{
    public class HomeController : Controller
    {
        IBreweryData _brewDb;

        public HomeController(IBreweryData db)
        {
            _brewDb = db;
        }

        public IActionResult Index()
        {
            var model = _brewDb.GetAllBeers();
            return View(model);
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
