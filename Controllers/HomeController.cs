﻿using IS413_Movie_Web_App_ZS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IS413_Movie_Web_App_ZS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /* Home Page*/
        public IActionResult Index()
        {
            return View();
        }

        /* Movie Collection Page that also filters out any movie named independence day*/
        public IActionResult My_Movies()
        {
            return View(Movie_Collection.Applications.Where(r => r.Movie_Title.ToUpper() != "INDEPENDENCE DAY"));
            
        }

        /* Movie Form Page*/
        [HttpGet]
        public IActionResult Add_Movie()
        {
            return View();
        }

        /* Movie Form Submission*/
        [HttpPost]
        public IActionResult Add_Movie(Add_Movie_Data new_Movie)
        {
            /* Logic to make sure that the movie Independence Day is never properly processed*/
            if (ModelState.IsValid == true)
            { 
                if (new_Movie.Movie_Title.ToUpper() != "INDEPENDENCE DAY")
                {
                    Movie_Collection.AddApplication(new_Movie);
                    return View("Confirmation", new_Movie);
                }
                else
                {
                    
                    return View("Confirmation", new_Movie);
                }
            }
            else
            {
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
