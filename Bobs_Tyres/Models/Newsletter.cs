using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class Newsletter
    {
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
        public string Image { get; set; }
    }
}