using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bobs_Tyres.Models;
using Bobs_Tyres.Security;

namespace Bobs_Tyres.Controllers
{
    public class ReviewsController : Controller
    {
        private Bobs_TyresContext db = new Bobs_TyresContext();

        // GET: Reviews
        public ActionResult Index(string message)
        {
            ViewBag.Success = message;
            var reviews = db.Reviews.Where(r => r.StatusID.Equals(2)).ToList();
            return View(reviews.OrderByDescending(r => r.Date).ToList());
        }

        public ActionResult AdminIndex(string message)
        {
            if (SessionPersister.Username != null)
            {
                ViewBag.Success = message;
                return View(db.Reviews.OrderBy(r => r.StatusID).ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Reviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReviewID,Name,Date,Rating,Testimonial,Location,StatusID")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.StatusID = 1;
                review.Date = DateTime.Now;
                db.Reviews.Add(review);
                db.SaveChanges();
                var success = "Your testimonial has been created and is now awaiting approval from our admin.";
                return RedirectToAction("Index", "Home", new { message = success });
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (SessionPersister.Username != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Review review = db.Reviews.Find(id);
                if (review == null)
                {
                    return HttpNotFound();
                }
                return View(review);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReviewID,Name,Date,Rating,Testimonial,Location")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminIndex");
            }
            return View(review);
        }

        public ActionResult Approve(int id)
        {
            Review review = db.Reviews.Find(id);
            review.StatusID = 2;
            db.SaveChanges();
            var success = "Testimonial by " + review.Name + " has been approved.";
            return RedirectToAction("AdminIndex", "Reviews", new { message = success});
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (SessionPersister.Username != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Review review = db.Reviews.Find(id);
                if (review == null)
                {
                    return HttpNotFound();
                }
                return View(review);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Review review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("AdminIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
