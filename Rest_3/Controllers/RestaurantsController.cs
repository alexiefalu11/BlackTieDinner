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


namespace Rest_3.Controllers
{
    public class RestaurantsController : Controller
    {
        private Rest_3Db db = new Rest_3Db();

        // GET: Restaurants
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManangeRestaurant))
            {
                ViewBag.displayMenu = "YES";
            }
            else
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Name", Value = "Name"});
                items.Add(new SelectListItem { Text = "City", Value = "City" });
                items.Add(new SelectListItem { Text = "ZipCode", Value = "ZipCode" });
                ViewData["SearchType"] = items;
                ViewBag.displayMenu = "NO";
            }
            TempData["Restaurants"] = db.Restaurants.ToList();

            return View(db.Restaurants.ToList());
        }

      
        [HttpPost]
        public ActionResult SearchType(string SearchType)
        {
            if (SearchType == "Name")
            {
                var rest = db.Restaurants.GroupBy(r => r.Name);
                return View("SearchName", rest.ToList());
            }
            else if (SearchType == "City")
            {
                var rest = db.Restaurants.ToList().GroupBy(r => r.City);
                return View("SearchCity", rest);
            }
            else if (SearchType == "Zipcode")
            {
                var rest = db.Restaurants.ToList().GroupBy(r => r.Zipcode);
                return View("SearchZip", rest);
            }
            else { return View("Index"); }
        }

        public ActionResult SearchName()
        {
            var names = db.Restaurants.OrderBy(r => r.Name);
            return View("Index",names);
        }

        public ActionResult SearchCity()
        {
            var city = db.Restaurants.OrderBy(r => r.City);
            return View("Index", city);
        }
        public ActionResult SearchZip()
        {
            var zip = db.Restaurants.OrderBy(r => r.Zipcode);
            return View("Index", zip);
        }
        public ActionResult SearchReview()
        {
                var reviews = db.Reviews.Include("Restaurant").GroupBy(a => a.RestaurantId).Select(s => new
            {
                Id = s.Key,
                AvgRating = s.Average(a => a == null ? 0 : a.Rating)
            }).OrderBy(a => a.AvgRating);

            var data = (from a in db.Restaurants
                       join b in reviews on a.Id equals b.Id
                       select new
                       {
                           AvgRating = b.AvgRating,
                           Id = a.Id,
                           City = a.City,
                           Email = a.Email,
                           Name = a.Name,
                           Phone = a.Phone,
                           State = a.State,
                           Zipcode = a.Zipcode,
                           Price = a.Price,
                           Website = a.Website
                       }).ToList()
                       .Select(x => new RestaurantRatingModel()
                       {
                           AvgRating = x.AvgRating,
                           Id = x.Id,
                           City = x.City,
                           Email = x.Email,
                           Name = x.Name,
                           Phone = x.Phone,
                           State = x.State,
                           Zipcode = x.Zipcode,
                           Price = x.Price,
                           Website = x.Website
                       });
            
            data = data.OrderBy(x => x.AvgRating);
            return View(data.ToList());
        }
        public ActionResult SearchReviewHigh()
        {
            var reviews = db.Reviews.Include("Restaurant").GroupBy(a => a.RestaurantId).Select(s => new
            {
                Id = s.Key,
                AvgRating = s.Average(a => a == null ? 0 : a.Rating)
            }).OrderBy(a => a.AvgRating);

            var data = (from a in db.Restaurants
                        join b in reviews on a.Id equals b.Id
                        select new
                        {
                            AvgRating = b.AvgRating,
                            Id = a.Id,
                            City = a.City,
                            Email = a.Email,
                            Name = a.Name,
                            Phone = a.Phone,
                            State = a.State,
                            Zipcode = a.Zipcode,
                            Price = a.Price,
                            Website = a.Website
                        }).ToList()
                       .Select(x => new RestaurantRatingModel()
                       {
                           AvgRating = x.AvgRating,
                           Id = x.Id,
                           City = x.City,
                           Email = x.Email,
                           Name = x.Name,
                           Phone = x.Phone,
                           State = x.State,
                           Zipcode = x.Zipcode,
                           Price = x.Price,
                           Website = x.Website
                       });

            data = data.OrderByDescending(x => x.AvgRating);
            return View("SearchReview",data.ToList());
        }
        public ActionResult Search(string search)
        {
            var rest = db.Restaurants.Where(r => r.Name.StartsWith(search) ||
            r.City.Contains(search) ||  r.Zipcode.Equals(search)) 
                .OrderByDescending(r => r.Name);
            return View("Index",rest);
        }

        // GET: Restaurants/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (User.IsInRole(RoleName.CanManangeRestaurant))
            {
                ViewBag.displayMenu = "YES";
            }
            else
            {
                ViewBag.displayMenu = "NO";
            }
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurants restaurants = db.Restaurants.Find(id);
            var reviews = db.Reviews.Where(r => r.RestaurantId.Equals(id.Value)).ToList();
            ViewBag.Reviews = reviews;
            if (restaurants == null)
            {
                return HttpNotFound();
            }
            return View(restaurants);
        }

        [AllowAnonymous]
        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        // GET: Restaurants/Create
        [Authorize(Roles = RoleName.CanManangeRestaurant)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManangeRestaurant)]
        public ActionResult Create([Bind(Include = "Id,Name,City,State,Zipcode,Email,Phone,Website,Price")] Restaurants restaurants)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurants);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurants);
        }

        // GET: Restaurants/Edit/5
        [Authorize(Roles = RoleName.CanManangeRestaurant)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurants restaurants = db.Restaurants.Find(id);
            if (restaurants == null)
            {
                return HttpNotFound();
            }
            return View(restaurants);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManangeRestaurant)]
        public ActionResult Edit([Bind(Include = "Id,Name,City,State,Zipcode,Email,Phone,Website,Price")] Restaurants restaurants)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurants).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurants);
        }

        // GET: Restaurants/Delete/5
        [Authorize(Roles = RoleName.CanManangeRestaurant)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurants restaurants = db.Restaurants.Find(id);
            if (restaurants == null)
            {
                return HttpNotFound();
            }
            return View(restaurants);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManangeRestaurant)]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurants restaurants = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurants);
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
