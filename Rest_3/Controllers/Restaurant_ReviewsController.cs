using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Rest_3.Data;
using Rest_3.Models;
using Microsoft.AspNet.Identity;
using Rest_3.Helper;

namespace Rest_3.Controllers
{
    public class Restaurant_ReviewsController : Controller
    {
        private Rest_3Db db = new Rest_3Db();

        // GET: Restaurant_Reviews
        public ActionResult Index()
        {
            ViewBag.User = User.Identity.GetUserName();
            if (User.IsInRole(RoleName.CanManangeRestaurant))
            {
                ViewBag.displayMenu = "YES";
            }
            else
            {
                ViewBag.displayMenu = "NO";
            }
         //   var reviews = db.Reviews.Include(r => r.Restaurants);
        /*    if(User.Identity.IsAuthenticated)
            {
                reviews = db.Reviews.Include(r => r.Restaurants).Where(a => a.ReviewersEmail.Equals(user));
               
            }
         */
            return View(db.Reviews.ToList());
        }
        [Authorize]
        public ActionResult MyReviews()
        {   var user = User.Identity.GetUserName();
            var reviews = db.Reviews.Include(r => r.Restaurants).Where(a => a.ReviewersEmail.Equals(user));
            ViewBag.User = User.Identity.GetUserName();
            return View("Index", reviews);
        }
        public ActionResult SearchRest()
        {
            
            ViewBag.user = User.Identity.GetUserName();
            var review = db.Reviews.Include(r => r.Restaurants).OrderBy(r => r.Restaurants.Name);
            return View("Index",review);
        }

        public ActionResult SearchReview()
        {
            ViewBag.user = User.Identity.GetUserName();
            var review = db.Reviews.Include(r => r.Restaurants).OrderBy(r => r.Rating);
            return View("Index", review);
        }

        public ActionResult SearchReviewHigh()
        {
            ViewBag.user = User.Identity.GetUserName();
            var review = db.Reviews.Include(r => r.Restaurants).OrderByDescending(r => r.Rating);
            return View("Index", review);
        }

        // GET: Restaurant_Reviews/Details/5
        public ActionResult Details(int? id)
        {
            var user = User.Identity.GetUserName();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant_Reviews restaurant_Reviews = db.Reviews.Find(id);
            if(restaurant_Reviews.ReviewersEmail == user)
            {
                ViewBag.user = "YES";
            }
            if (restaurant_Reviews == null)
            {
                return HttpNotFound();
            }
            return View(restaurant_Reviews);
        }

        // GET: Restaurant_Reviews/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name");
            var user = User.GetName();
            var userEmail = User.Identity.GetUserName();
            ViewBag.User = user;
            ViewBag.userEmail = userEmail;
            return View();
        }

        // POST: Restaurant_Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Rating,Title,Comment,ReviewersName,DateCreated,RestaurantId,ReviewersEmail")] Restaurant_Reviews restaurant_Reviews)
        {
            
            if (ModelState.IsValid)
            {
                db.Reviews.Add(restaurant_Reviews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurant_Reviews.RestaurantId);
            return View(restaurant_Reviews);
        }

        // GET: Restaurant_Reviews/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            var userEmail = User.Identity.GetUserName();
            var user = User.GetName();
            ViewBag.User = user;
            ViewBag.userEmail = userEmail;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant_Reviews restaurant_Reviews = db.Reviews.Find(id);
            if (restaurant_Reviews == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rest = restaurant_Reviews.Id;
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurant_Reviews.RestaurantId);
            return View(restaurant_Reviews);
        }

        // POST: Restaurant_Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Rating,Title,Comment,ReviewersName,DateCreated,RestaurantId,ReviewersEmail")] Restaurant_Reviews restaurant_Reviews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant_Reviews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RestaurantId = new SelectList(db.Restaurants, "Id", "Name", restaurant_Reviews.RestaurantId);
            return View(restaurant_Reviews);
        }

        // GET: Restaurant_Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant_Reviews restaurant_Reviews = db.Reviews.Find(id);
            if (restaurant_Reviews == null)
            {
                return HttpNotFound();
            }
            return View(restaurant_Reviews);
        }

        // POST: Restaurant_Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant_Reviews restaurant_Reviews = db.Reviews.Find(id);
            db.Reviews.Remove(restaurant_Reviews);
            db.SaveChanges();
            return RedirectToAction("Index");
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
