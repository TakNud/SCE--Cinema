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
using Microsoft.AspNet.Identity;

namespace SCECinema.Controllers
{
    public class ShowingsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Showings
        public ActionResult Index(int? id)
        {
            if (id == null || id == -1)
            {
                return View(db.Showings.ToList());
            }
            if (id == 0)
            {
            
                int Day = DateTime.Now.Day;
                return View(db.Showings.Where(u => u.StartTime.Day == Day).ToList());
            }
            Movie m = db.Movies.Find(id);
            if (m == null)
            {
                return HttpNotFound();
            }
            
            var query = from r in db.Showings select r;
            if (m != null)
            {
                query = query.Where(r => r.Movie.Id == id);
            }
            List<Showing> ShowingsToDisplay = query.ToList();
            ViewBag.SelectedShowingsCount = ShowingsToDisplay.Count();
            ViewBag.TotalMovieShowingsCount = db.Showings.ToList().Count();

            return View(ShowingsToDisplay.OrderBy(r => r.StartTime));
            
            return View("index");
        }

    


        // GET: Showings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            return View(showing);
        }

        // GET: Showings/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.AllMoviesList = GetAllMovies();
            return View();
        }

        // POST: Showings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ShowingID,StartTime,TheatreNum,Price,SeatList,Movie_Id")] Showing showing, int SearchMovieID)
        {

            if (ModelState.IsValid && showing.StartTime > DateTime.Now)
            {
                Movie m = db.Movies.FirstOrDefault(x => x.Id == SearchMovieID);
                showing.EndTime = showing.StartTime.AddMinutes(m.Duration);
                showing.Movie = m;
                //showing.Price = Price;
                db.Showings.Add(showing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AllMoviesList = GetAllMovies();
            ViewBag.ShowingInPastError = "You've scheduled this Showing in the past. Pick a future start time.";
            return View(showing);
        }

        // GET: Showings/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            ViewBag.AllMoviesList = GetAllMovies();
            return View(showing);
        }

        // POST: Showings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ShowingID,StartTime,TheatreNum")] Showing showing)
        {
            if (ModelState.IsValid)
            {
                Showing show = db.Showings.Find(showing.ShowingID);
                show.StartTime = showing.StartTime;
                show.TheatreNum = showing.TheatreNum;
                //Console.WriteLine(showing..Duration);
                show.EndTime = show.StartTime.AddMinutes(show.Movie.Duration) ;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(showing);
        }

        // GET: Showings/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Showing showing = db.Showings.Find(id);
            if (showing == null)
            {
                return HttpNotFound();
            }
            return View(showing);
        }

        // POST: Showings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Showing showing = db.Showings.Find(id);


            db.Showings.Remove(showing);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public SelectList GetAllMovies()
        {
            List<Movie> Movies = db.Movies.ToList();

            SelectList AllMovies = new SelectList(Movies.OrderBy(m => m.Title), "Id", "Title");
            return AllMovies;

        }

        public ActionResult CopyMovies(int id)
        {

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
