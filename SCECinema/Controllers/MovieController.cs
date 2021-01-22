using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCECinema.DAL;
using SCECinema.Models;

namespace SCECinema.Controllers
{
    public class MovieController : Controller
    {
        private AppDbContext db= new AppDbContext();

      
        // GET: Movies
        
        public ActionResult Index()
        {
            List<Movie> MoviesToDisplay = new List<Movie>();

            var query = from r in db.Movies select r;
         
            MoviesToDisplay = query.ToList();

            ViewBag.SelectedMoviesCount = MoviesToDisplay.Count();
            ViewBag.TotalMoviesCount = db.Movies.ToList().Count();

            return View(MoviesToDisplay.OrderBy(r => r.Title));
        }


        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "MovieID,Title,Synopsis,Duration,AgeLimit,MovieImage,GenreId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
         
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllGenres = GetAllGenres();
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Title,Synopsis,Duration,AgeLimit,MovieImage")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                Movie movieToChange = db.Movies.Find(movie.Id);
               
              
                movieToChange.Title = movie.Title;
                movieToChange.Synopsis = movie.Synopsis;
                movieToChange.Duration = movie.Duration;
                movieToChange.AgeLimit = movie.AgeLimit;
                movieToChange.MovieImage = movie.MovieImage;
                
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie.Showing == null) {
                db.Movies.Remove(movie);
                db.SaveChanges();
            return RedirectToAction("Index");
            }
            else
            {
                return Content("Cannot delete movie with activity showing ! ");
            }
        }

        protected override void Dispose(bool disposing)
        {
          
                db.Dispose();
  
        }
        public MultiSelectList GetAllGenres()
        {
            List<Genre> allGenres = db.Genres.OrderBy(g => g.Name).ToList();

            MultiSelectList selGenres = new MultiSelectList(allGenres, "GenreID", "Name");

            return selGenres;
        }

    }
}
