using MVC.Models.Movie;
using MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieMVC.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            var service = new MovieService();
            var model = service.GetMovies();

            return View(model);
        }

        //GET: Note
        //Note/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Note
        //Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreateMovieService();
                if (service.CreateMovie(model))
                {
                    TempData["SaveResult"] = "Your movie was created.";
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Movie could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateMovieService();
            var model = service.GetMovieById(id);
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateMovieService();
            var detail = service.GetMovieById(id);
            var model = new MovieEdit
            {
                Title = detail.Title,
                Genre = detail.Genre,
                ReleaseDate = detail.ReleaseDate,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MovieEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.Id != id)
            {
                ModelState.AddModelError("", "Id Mismatch.");
                return View(model);
            }

            var service = CreateMovieService();
            if (service.EditMovie(model))
            {
                TempData["SaveResult"] = "Your movie was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your movie could not be updated.");
            return View(model);
        }
        //GET: Delete
        //Note/Delte
        public ActionResult Delete(int id)
        {
            var svc = CreateMovieService();
            var model = svc.GetMovieById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMovie(int id)
        {
            var service = CreateMovieService();
            service.DeleteMovie(id);
            TempData["SaveResult"] = "Your movie was deleted";

            return RedirectToAction("Index");
        }
        private MovieService CreateMovieService()
        {
            var service = new MovieService();
            return service;
        }
    }
}