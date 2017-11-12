using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rest_3.Models
{
    public class RestaurantRatingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }      
        public string Zipcode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
       // [DataType(DataType.Url)]
        public string Website { get; set; }
     //   [Range(0, 5, ErrorMessage = "Price Range should be between 0 and 5")]
        public int Price { get; set; }
        [DisplayFormat(DataFormatString = "{0:n1}", ApplyFormatInEditMode = true)]
        public decimal AvgRating { get; set; }
    }
}