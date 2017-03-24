using Bobs_Tyres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bobs_Tyres.Controllers
{
    public class HomeController : Controller
    {

        private Bobs_TyresContext db = new Bobs_TyresContext();
        public ActionResult Index()
        {
            ViewBag.LatestNews = db.LatestNews.ToList();
            ViewBag.LatestNewsCount = db.LatestNews.ToList().Count();
            ViewBag.Reviews = db.Reviews.ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}