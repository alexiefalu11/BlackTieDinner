using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rest_3.Models
{
    public class Restaurants
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]

        [StringLength(11, MinimumLength = 5, ErrorMessage = "Invalid Zipcode Range")]
        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 10, ErrorMessage = "Invalid Phone Number  Range")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Price Range should be between 0 and 5")]
        public int Price { get; set; }

        public virtual ICollection<Restaurant_Reviews> Reviews { get; set; }
        public virtual ICollection<Cuisines> Cuisines { get; set; }
    }
}