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
using System.IO;

namespace Bobs_Tyres.Controllers
{
    public class LatestNewsController : Controller
    {
        private Bobs_TyresContext db = new Bobs_TyresContext();

        // GET: LatestNews
        public ActionResult Index(string message, string errormessage)
        {
            if (SessionPersister.Username != null)
            {
                ViewBag.Success = message;
                ViewBag.Error = errormessage;
                return View(db.LatestNews.ToList());
            }
            return RedirectToAction("Index", "Home");            
        }

        // GET: LatestNews/Create
        public ActionResult Create()
        {
            if (SessionPersister.Username != null)
            {
                var numNews = db.LatestNews.Count();
                if(numNews == 10)
                {
                    var errormessage = "You can't have more than 10 news items - remove an existing news item to add a new one.";
                    return RedirectToAction("Index", new { errormessage = errormessage }); 
                }
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: LatestNews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LatestNewsID,Title,Image,Description,ButtonLink")] LatestNews latestNews)
        {
            if (ModelState.IsValid)
            {
                if(Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("../Content/Images/LatestNews"), fileName);

                        if(db.LatestNews.Where(ln => ln.Image.Equals(fileName)).FirstOrDefault() != null)
                        {
                            ViewBag.Error = "An image with this name already exists, please rename the file or select it from the list of existing images.";
                            return View(latestNews);
                        }
                        else
                        {
                            latestNews.Image = fileName;
                        }
                        file.SaveAs(path);
                    }
                    else if (String.IsNullOrEmpty(latestNews.Image))
                    {
                        latestNews.Image = "tyre.png";
                    }
                }
                else if (String.IsNullOrEmpty(latestNews.Image))
                {
                    latestNews.Image = "tyre.png";
                }

                db.LatestNews.Add(latestNews);
                db.SaveChanges();
                ModelState.Clear();
                var success = "News item successfully added";
                return RedirectToAction("Index", new { message = success});
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
        public ActionResult Edit([Bind(Include = "LatestNewsID,Title,Image,Description,ButtonLink")] LatestNews latestNews)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("../Content/Images/LatestNews"), fileName);

                        if (db.LatestNews.Where(ln => ln.Image.Equals(fileName)).FirstOrDefault() != null)
                        {
                            ViewBag.Error = "An image with this name already exists, please rename the file or select it from the list of existing images.";
                            return View(latestNews);
                        }
                        else
                        {
                            latestNews.Image = fileName;
                        }
                        file.SaveAs(path);
                    }
                }

                ModelState.Clear();
                db.Entry(latestNews).State = EntityState.Modified;
                db.SaveChanges();
                var success = "News item successfully altered";
                return RedirectToAction("Index", new { message = success});
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
