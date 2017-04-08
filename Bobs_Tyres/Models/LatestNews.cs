﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class LatestNews
    {
        [Key]
        public int LatestNewsID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}