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
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Date { get; set; }
        public int Rating { get; set; }
        public string Testimonial { get; set; }
        public string Location { get; set; }

    }
}