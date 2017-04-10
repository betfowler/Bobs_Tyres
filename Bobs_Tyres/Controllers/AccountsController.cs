using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bobs_Tyres.Models;
using Bobs_Tyres.ViewModels;
using Bobs_Tyres.Security;
using Facebook;
using System.IO;

namespace Bobs_Tyres.Controllers
{
    public class AccountsController : Controller
    {
        private Bobs_TyresContext db = new Bobs_TyresContext();

        // GET: Accounts
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Newsletter(string image, string message, string title, string error)
        {
            Newsletter newsletter = new Newsletter();
            newsletter.Message = message;
            newsletter.Title = title;
            ViewBag.Image = image;
            ViewBag.Error = error;
            return View(newsletter);
        }

        [HttpPost]
        public JsonResult AjaxRemoveImage (string imageName)
        {
            var path = HttpContext.Server.MapPath("/Content/Images/Newsletter/" + imageName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Json(new { message="success"});
            }
            return Json(new { message = "error" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Newsletter([Bind(Include = "Title,Message,Image")] Newsletter newsletter)
        {
            string imageUrl = "localhost:58724/Content/Images/Newsletter/" + newsletter.Image;
            return View();
        }
        [HttpPost]
        public ActionResult NewsImageUpload(Newsletter newsletter)
        {
            var fileName = "";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("../Content/Images/Newsletter"), fileName);

                    DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~\Content\Images\Newsletter"));
                    var pictures = directory.GetFiles().ToList();
                    foreach(var picture in pictures)
                    {
                        var name = picture.ToString();
                        if(name == fileName)
                        {
                            fileName = "";
                            string message = "An image with this name already exists, please rename the file or select it from the list of existing images.";
                            return RedirectToAction("Newsletter", new { image = fileName, message = newsletter.Message, title = newsletter.Title, error = message });
                        }
                    }

                    DirectoryInfo newsdirectory = new DirectoryInfo(Server.MapPath(@"~\Content\Images\LatestNews"));
                    var newspictures = newsdirectory.GetFiles().ToList();
                    foreach (var picture in newspictures)
                    {
                        var name = picture.ToString();
                        if (name == fileName)
                        {
                            fileName = "";
                            string message = "An image with this name already exists, please rename the file or select it from the list of existing images.";
                            return RedirectToAction("Newsletter", new { image = fileName, message = newsletter.Message, title = newsletter.Title, error = message });
                        }
                    }

                    file.SaveAs(path);
                }
            }
            return RedirectToAction("Newsletter", new { image = fileName, message = newsletter.Message, title = newsletter.Title });
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            if(SessionPersister.Username != null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Accounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,Username,Password")] Account account)
        {
            if (ModelState.IsValid)
            {
                account.Password = Hashing.ComputeHash(account.Password);
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }


        [HttpPost]
        public ActionResult Login(AccountViewModel avm, AccountModel am)
        {
            if(string.IsNullOrEmpty(avm.Account.Username) || string.IsNullOrEmpty(avm.Account.Password) || am.login(avm.Account.Username, avm.Account.Password) == false)
            {
                ViewBag.Error = "Account is invalid";
                return View("Index");
            }
            SessionPersister.Username = avm.Account.Username;
            return View("Home");
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult LogOut()
        {
            SessionPersister.Username = null;
            return RedirectToAction("Index", "Home");
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
