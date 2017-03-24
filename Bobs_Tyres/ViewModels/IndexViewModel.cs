using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bobs_Tyres.Models;

namespace Bobs_Tyres.ViewModels
{
    public class IndexViewModel
    {
        public Review Review { get; set; }
        public LatestNews LatestNews { get; set; }
    }
}