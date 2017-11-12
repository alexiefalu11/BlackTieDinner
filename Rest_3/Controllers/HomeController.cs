using Rest_3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rest_3.Models;

namespace Rest_3.Controllers
{
    public class HomeController : Controller
    {
        private Rest_3Db db = new Rest_3Db();

        public ActionResult Index()
        {
            var reviews = db.Reviews.Include("Restaurant").GroupBy(a => a.RestaurantId).Select(s => new
            {
                Id = s.Key,
                AvgRating = s.Average(a => a.Rating)
            }).OrderByDescending(a => a.AvgRating).Take(3);

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
                       .Select( x => new RestaurantRatingModel()
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
        
            return View(data.ToList());
           
           
        }

        public decimal Average(int id)
        {
            var reviewsdb = db.Reviews;
            var avg = 0.0m;
            try
            {
                var reviews = db.Restaurants.Find(id);
              
                avg = reviews.Reviews.Average(y => y.Rating);
            }
            catch { }
            return avg;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}