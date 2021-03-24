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

        //Creates a private variable after the MovieDBContext class to communicate with the database
        private MovieDBContext _context { get; set; }

        //Constructor for the Home Controller
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
        [HttpGet]
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

        /* Movie Form Submission to add a new movie*/
        [HttpPost]
        public IActionResult Add_Movie(Add_Movie_Data new_Movie)
        {
            /* Logic to make sure that the movie Independence Day is never properly processed*/
            if (ModelState.IsValid == true)
            { 
                if (new_Movie.Movie_Title.ToUpper() != "INDEPENDENCE DAY")
                {
                    //Communicates with the sqlite database through the private context object to modify data in the database
                    _context.Movies.Add(new_Movie);
                    _context.SaveChanges();

                    return View("Confirmation", new_Movie);
                }
                else
                {
                    //Return the confirmation page with error messages for tyring to enter "independence day" as a movie
                    return View("Confirmation", new_Movie);
                }
            }
            else
            {
                //Reload the page in a way that allows mistyped info to be called-out
                return View(new_Movie);
            }
        }

        //Action that submits a form for an edited entry in the database
        [HttpPost]
        public IActionResult Edit_Movie(Add_Movie_Data movie_to_edit)
        {
            if (ModelState.IsValid == true) {
                if (movie_to_edit.Movie_Title.ToUpper() != "INDEPENDENCE DAY") { 

                    //Create an Iqueryable object that returns the one specific record that is being edited
                    IQueryable<Add_Movie_Data> editing_movie = _context.Movies.Where(p => p.MovieID == movie_to_edit.MovieID);

                    //loop to edit the data of the desired record
                    foreach (var x in editing_movie)
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

                    //Then return a customized confirmation page that confirms that the record has been modified/edited
                    return View("Edit_Confirmation", movie_to_edit);
                }

                else
                {
                    //Return the confirmation page with error messages for tyring to enter "independence day" as a movie
                    return View("Edit_Confirmation", movie_to_edit);
                }
            }

            else
            {
                return View(movie_to_edit);
            }
        }

        //Action for the My_Movies page that takes the user to a page for editing the desired record
        [HttpPost]
        public IActionResult My_Movies(Add_Movie_Data movie_to_edit)
        {
            return View("Edit_Movie", movie_to_edit);
        }

        //Action that removes the desired record from the sqlite database
        [HttpPost]
        public IActionResult Remove_Movie(Add_Movie_Data movie_to_remove)
        {
            //IQueryable object that finds the desired record in the sqlite database to remove
            IQueryable<Add_Movie_Data> removing_record = _context.Movies.Where(p => p.MovieID == movie_to_remove.MovieID);

            //loop to remove the record in the database
            foreach (var x in removing_record)
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
