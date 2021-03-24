using IS413_Movie_Web_App_ZS.Models;
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

        private MovieDBContext _context { get; set; }

        public HomeController(ILogger<HomeController> logger, MovieDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        /* Home Page*/
        public IActionResult Index()
        {
            return View();
        }

        /* Link to Podcasts Page*/
        public IActionResult My_Podcasts()
        {
            return View();
        }

        /* Movie Collection Page that also filters out any movie named independence day*/
        public IActionResult My_Movies()
        {
            IQueryable<Add_Movie_Data> Movie_DB = _context.Movies.Where(r => r.Movie_Title.ToUpper() != "INDEPENDENCE DAY");
            return View(Movie_DB);
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
                    _context.Movies.Add(new_Movie);
                    _context.SaveChanges();
                    //Movie_Collection.AddApplication(new_Movie);
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

        [HttpPost]
        public IActionResult Edit_Movie(Add_Movie_Data movie_to_edit)
        {
            IQueryable<Add_Movie_Data> wumbo = _context.Movies.Where(p => p.MovieID == movie_to_edit.MovieID);

            foreach (var x in wumbo)
            {
                x.Movie_Title = movie_to_edit.Movie_Title;
                x.Category = movie_to_edit.Category;
                x.Director = movie_to_edit.Director;
                x.Rating = movie_to_edit.Rating;
                x.Year = movie_to_edit.Year;
                x.Lent_To = movie_to_edit.Lent_To;
                x.Edited = movie_to_edit.Edited;
                x.Notes = movie_to_edit.Notes;
            }

            _context.SaveChanges();

            return View("Edit_Confirmation", movie_to_edit);
        }

        [HttpPost]
        public IActionResult My_Movies(Add_Movie_Data movie_to_edit)
        {
            return View("Edit_Movie", movie_to_edit);
        }

        [HttpPost]
        public IActionResult Remove_Movie(Add_Movie_Data movie_to_remove)
        {
            IQueryable<Add_Movie_Data> wumbo = _context.Movies.Where(p => p.MovieID == movie_to_remove.MovieID);

            foreach (var x in wumbo)
            {
                _context.Movies.Remove(x);
            }

            _context.SaveChanges();

            return View("Edit_Confirmation", movie_to_remove);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
