using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    public class Bobs_TyresContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Bobs_TyresContext() : base("name=Bobs_TyresContext")
        {
        }

        public System.Data.Entity.DbSet<Bobs_Tyres.Models.Review> Reviews { get; set; }

        public System.Data.Entity.DbSet<Bobs_Tyres.Models.LatestNews> LatestNews { get; set; }

        public System.Data.Entity.DbSet<Bobs_Tyres.Models.Account> Accounts { get; set; }
        public System.Data.Entity.DbSet<Bobs_Tyres.Models.ReviewStatus> ReviewStatus { get; set; }
        public System.Data.Entity.DbSet<Bobs_Tyres.Models.Subscriber> Subscribers { get; set; }
    }
}
