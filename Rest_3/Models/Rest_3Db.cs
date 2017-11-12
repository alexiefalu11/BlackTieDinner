using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Rest_3.Models;

namespace Rest_3.Data
{
    public class Rest_3Db : DbContext
    {
        public Rest_3Db() : base("Rest_Db3")
        {

        }

        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<Restaurant_Reviews> Reviews { get; set; }
        public DbSet<Cuisines> Cuisines { get; set; }

        public System.Data.Entity.DbSet<Rest_3.Models.RestaurantRatingModel> RestaurantRatingModels { get; set; }
    }
}