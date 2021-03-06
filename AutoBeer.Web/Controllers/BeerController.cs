﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBeer.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoBeer.Web.Controllers
{
    public class BeerController : Controller
    {
        IBreweryData _brewDb;
        public BeerController(IBreweryData db)
        {
            _brewDb = db;
        }

        [Route("/Beer")]
        [Route("/Beer/Index")]
        public IActionResult Index()
        {
            var model = _brewDb.GetAllBeers();
            return View(model);
        }

        [Route("/Beer/{id}")]
        public IActionResult Details(int id)
        {
            var model = _brewDb.GetBeer(id);
            return View(model);
        }
    }
}