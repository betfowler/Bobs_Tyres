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
using System.Net.Mail;
using System.Threading.Tasks;
using System.Reflection;

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

        public ActionResult Newsletter(string image, string message, string title, string error, string success)
        {
            Newsletter newsletter = new Newsletter();
            newsletter.Message = message;
            newsletter.Title = title;
            ViewBag.Image = image;
            ViewBag.Error = error;
            ViewBag.Success = success;
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

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public class NewsletterAttribute : ActionNameSelectorAttribute
        {
            public string Name { get; set; }
            public string Argument { get; set; }

            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                var isValidName = false;
                var keyValue = string.Format("{0}:{1}", Name, Argument);
                var value = controllerContext.Controller.ValueProvider.GetValue(keyValue);
                if (value != null)
                {
                    controllerContext.Controller.ControllerContext.RouteData.Values[Name] = Argument;
                    isValidName = true;
                }

                return isValidName; 
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Newsletter(Name = "action", Argument = "Send")]
        public async Task<ActionResult> Send([Bind(Include = "Title,Message,Image,Folder")] Newsletter newsletter)
        {
            if (!String.IsNullOrEmpty(newsletter.Image))
            {
                if (newsletter.Image.Substring(0, Math.Min(newsletter.Image.Length, 4)) != "http")
                {
                    var http = "http://";
                    //url needs to be changed in Newsletter.cshtml line 203 + 206
                    if (newsletter.Folder == "" || newsletter.Folder == "Newsletter")
                    {
                        newsletter.Image = http + "bethanyfowler-001-site2.btempurl.com/Content/Images/Newsletter/" + newsletter.Image;
                    }
                    else
                    {
                        newsletter.Image = http + "bethanyfowler-001-site2.btempurl.com/Content/Images/LatestNews/" + newsletter.Image;
                    }

                }
            }

            //send email
            var body = "";
            string messageBody = "";
            if (!String.IsNullOrEmpty(newsletter.Image))
            {
                body = "<table style='text-align:center; margin: auto'><tr><th><img style='margin: auto; width:40%'src='http://bethanyfowler-001-site2.btempurl.com/Content/Images/logo.jpg'/></th></tr><tr><h1>Bobs Tyres and Garage Services Newsletter</h1></tr><tr><h2>{0}</h2></tr><tr><th><img style='width: 50%' src='{2}'></th></tr><tr><h3>{1}</h3></tr><tr><th>Call us on: <p>01460 72037<p></th></tr><tr><td>www.bobstyresltd.co.uk</td></tr><tr><td>Bobs Tyres Ltd, Blacknell Lane Trading Estate, Crewkerene, Somerset, TA18 7HE</td></tr></table>";
                messageBody = string.Format(body, newsletter.Title.ToString(), newsletter.Message.ToString(), newsletter.Image);
            }
            else
            {
                body = "<table style='text-align:center; margin: auto'><tr><th><img style='margin: auto; width:40%'src='http://bethanyfowler-001-site2.btempurl.com/Content/Images/logo.jpg'/></th></tr><tr><h1>Bobs Tyres and Garage Services Newsletter</h1></tr><tr><h2>{0}</h2></tr><tr><h3>{1}</h3></tr><tr><th>Call us on: <p>01460 72037<p></th></tr><tr><td>www.bobstyresltd.co.uk</td></tr><tr><td>Bobs Tyres Ltd, Blacknell Lane Trading Estate, Crewkerene, Somerset, TA18 7HE</td></tr></table>";
                messageBody = string.Format(body, newsletter.Title.ToString(), newsletter.Message.ToString());
            }
            
            string to = "bethany.fowler14@gmail.com";
            string from = "bobstyresandgarageservices@gmail.com";
            string subject = "Bobs Tyres";

            await SendMessage(to, from, messageBody, subject);
            var success = "Newsletter successfully sent.";
            return RedirectToAction("Newsletter", new { image = newsletter.Image, message = newsletter.Message, title = newsletter.Title, success = success });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Newsletter(Name ="action", Argument = "Upload")]
        public ActionResult Upload(Newsletter newsletter)
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
                    foreach (var picture in pictures)
                    {
                        var name = picture.ToString();
                        if (name == fileName)
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


        public async Task<ActionResult> SendMessage(string to, string from, string body, string subject)
        {
            //send email
            var message = new MailMessage();
            message.Body = body;
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "bobstyresandgarageservices@gmail.com",
                    Password = "seryTB0bs"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(message);
            }
            return RedirectToAction("Index");
        }
    }
}
