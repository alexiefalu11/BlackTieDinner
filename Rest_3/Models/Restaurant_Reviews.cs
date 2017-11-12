using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Rest_3.Models
{
    public class Restaurant_Reviews
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Range(0, 10, ErrorMessage = "Rating Range should be between 0 and 10")]
        public decimal Rating { get; set; }
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
        [Display(Name = "Reviewer's Name")]
        [StringLength(50)]
        public string ReviewersName { get; set; }
        [StringLength(100)]
        public string ReviewersEmail { get; set; }
        [Display(Name = "Date Of Review")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [ForeignKey("Restaurants")]
        [Display(Name = "Restaurant Name")]
        public int RestaurantId { get; set; }

        public virtual Restaurants Restaurants { get; set; }
    }
}