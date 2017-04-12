using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bobs_Tyres.Models;

namespace Bobs_Tyres.ViewModels
{
    public class ContactViewModel
    {
        public Contact contact { get; set; }
        public Subscriber subscriber { get; set; }

    }
}