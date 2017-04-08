using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class ReviewStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string Status { get; set; }

        public virtual ICollection <Review> Reviews { get; set; }
    }
}