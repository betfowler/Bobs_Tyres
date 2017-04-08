using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [Display(Name = "Your Name/Company")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        [DataType(DataType.MultilineText)]
        public string Testimonial { get; set; }
        public string Location { get; set; }
        public int StatusID { get; set; }

        public virtual ReviewStatus ReviewStatus { get; set; }
    }
}