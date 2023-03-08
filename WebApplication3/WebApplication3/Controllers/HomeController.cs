using CoreServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Model;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDataContext context;

        public HomeController(MovieDataContext context)
        {
            this.context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            LogTimestamp();
            var movies = this.context.Movies.Select(m => new MovieViewModel
            {
                Title = m.Title,
                Year = m.Year.ToString(),
                Summary = m.Summary
            });

            return View(movies);
        }

        void LogTimestamp()
        {
            System.IO.File.WriteAllTextAsync("timestamp.txt", DateTime.Now.ToString());
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            using (var db = context)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
            }
            return Index();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteMovie(string title)
        {
            using (var db = context)
            {
                var movie = db.Movies.Where(b => b.Title == title).FirstOrDefault();
                if (movie != null) db.Movies.Remove(movie);
                db.SaveChanges();
            }
            return Index();
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateMovie([FromBody] Movie movie)
        {
            using (var db = context)
            {
                var toupdate = db.Movies.Where(b => b.Title == movie.Title).FirstOrDefault();
                if (toupdate != null)
                {
                    toupdate.Title = movie.Title;
                    toupdate.Year = movie.Year;
                    toupdate.Summary = movie.Summary;
                    db.Movies.Update(toupdate);
                    db.SaveChanges();
                }
            }
            return Index();
        }

        [Authorize]
        public IActionResult GetMovies()
        {
            Console.WriteLine("testing add");

            return Index();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Privacy2()
        {
            return View();
        }
    }

    public class MovieViewModel
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Summary { get; set; }
        public string Actors { get; set; }
    }
}
