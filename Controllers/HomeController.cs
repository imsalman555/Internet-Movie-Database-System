using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EADProject.Models;
using Microsoft.Ajax.Utilities;

namespace EADProject.Controllers
{
    public class HomeController : Controller
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();
        List<movies> moviesByGenre = new List<movies>();
        public ActionResult Index()
        {
            moviesByGenre = dc.movies.GroupBy(m=>m.Genre).Select(g => g.First()).ToList();
            return View(moviesByGenre);
        }

        public JsonResult GetData()
        {
            string genre = Request["Genre"];
            moviesByGenre = dc.movies.Where(movie=>movie.Genre.Equals(genre)).ToList();
            string json = string.Empty;
            if (moviesByGenre.Count != 0)
            {
                json += "{\"movies\":[";
                for (int idx = 0; idx < moviesByGenre.Count; idx++)
                {
                    json += "{";
                    json += "\"Id\":" + moviesByGenre[idx].Id;
                    json += ", \"Title\":\"" + moviesByGenre[idx].Title.Trim() + "\"";
                    json += ", \"Genre\":\"" + moviesByGenre[idx].Genre.Trim() + "\"";
                    json += ", \"Duration\":" + moviesByGenre[idx].Duration;
                    json += ", \"Release_Year\":" + moviesByGenre[idx].Release_Year;
                    json += ", \"Rating\":" + moviesByGenre[idx].Rating;
                    if (idx == moviesByGenre.Count - 1)
                        json += "}";
                    else
                        json += "},";
                }
                json += "]}";
            }
            return Json(json);
        }

        public ActionResult AddMovie()
        {
            return View();
        }

        public ActionResult Add()
        {
            string title = Request["title"];
            int duration = Convert.ToInt32(Request["duration"]);
            string genre = Request["genre"];
            int year = Convert.ToInt32(Request["year"]);
            float rating = Convert.ToInt32(Request["rating"]);
            movies m = new movies();
            m.Title = title;
            m.Duration = duration;
            m.Genre = genre;
            m.Release_Year = year;
            m.Rating = rating;

            dc.movies.InsertOnSubmit(m);
            dc.SubmitChanges();

            return RedirectToAction("AddMovie");
        }
        public ActionResult EditMovies()
        {
            return View(dc.movies.ToList());
        }

        public ActionResult Edit()
        {
            int id = Convert.ToInt32(Request["Id"]);
            return View(dc.movies.First(m => m.Id == id));
        }

        public ActionResult EditDone()
        {
            int id = Convert.ToInt32(Request["Id"]);
            var movie = dc.movies.First(m => m.Id == id);
            movie.Title = Request["title"];
            movie.Duration = Convert.ToInt32(Request["duration"]);
            movie.Genre = Request["genre"];
            movie.Release_Year = Convert.ToInt32(Request["year"]);
            movie.Rating = Convert.ToInt32(Request["rating"]);

            dc.SubmitChanges();
            return RedirectToAction("EditMovies");
        }
    }
}