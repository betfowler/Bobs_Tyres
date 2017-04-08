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
    public class LatestNewsController : Controller
    {
        private Bobs_TyresContext db = new Bobs_TyresContext();

        // GET: LatestNews
        public ActionResult Index()
        {
            if (SessionPersister.Username != null)
            {
                return View(db.LatestNews.ToList());
            }
            return RedirectToAction("Index", "Home");            
        }

        // GET: LatestNews/Details/5
        public ActionResult Details(int? id)
        {
            if (SessionPersister.Username != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LatestNews latestNews = db.LatestNews.Find(id);
                if (latestNews == null)
                {
                    return HttpNotFound();
                }
                return View(latestNews);
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: LatestNews/Create
        public ActionResult Create()
        {
            if (SessionPersister.Username != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: LatestNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LatestNewsID,Title,Image,Description")] LatestNews latestNews)
        {
            if (ModelState.IsValid)
            {
                db.LatestNews.Add(latestNews);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(latestNews);
        }

        // GET: LatestNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (SessionPersister.Username != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LatestNews latestNews = db.LatestNews.Find(id);
                if (latestNews == null)
                {
                    return HttpNotFound();
                }
                return View(latestNews);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: LatestNews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LatestNewsID,Title,Image,Description")] LatestNews latestNews)
        {
            if (ModelState.IsValid)
            {
                db.Entry(latestNews).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(latestNews);
        }

        // GET: LatestNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (SessionPersister.Username != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                LatestNews latestNews = db.LatestNews.Find(id);
                if (latestNews == null)
                {
                    return HttpNotFound();
                }
                return View(latestNews);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: LatestNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LatestNews latestNews = db.LatestNews.Find(id);
            db.LatestNews.Remove(latestNews);
            db.SaveChanges();
            return RedirectToAction("Index");
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
