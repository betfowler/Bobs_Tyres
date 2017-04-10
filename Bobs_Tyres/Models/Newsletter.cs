using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class Newsletter
    {
        [Key]
        public int NewsletterID { get; set; }
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public string Image { get; set; }
        public string Folder { get; set; }
    }
}