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
        public ActionResult Index(string message)
        {
            ViewBag.Success = message;
            ViewBag.LatestNews = db.LatestNews.ToList();
            ViewBag.LatestNewsCount = db.LatestNews.ToList().Count();
            ViewBag.Reviews = db.Reviews.Where(r => r.StatusID.Equals(2)).OrderByDescending(r => r.Date).ToList();
            var numReviews = 0;
            var totalReview = 0;
            foreach(var review in db.Reviews.Where(r => r.StatusID.Equals(2)).ToList())
            {
                numReviews = numReviews + 1;
                totalReview = totalReview + review.Rating;
            }
            ViewBag.ReviewAverage = Math.Ceiling((double)totalReview / numReviews);
            ViewBag.NumReview = numReviews;
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