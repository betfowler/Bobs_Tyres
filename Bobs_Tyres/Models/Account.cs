using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class Account
    {
        [Key]
        public int userID { get; set; }
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}