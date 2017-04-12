using Bobs_Tyres.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public ActionResult Contact(string message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                ViewBag.Success = message;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> Contact([Bind(Include = "customerName,emailAddress,contactNumber,message,newsletter")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                string EncodedResponse = Request.Form["g-Recaptcha-Response"];
                bool IsCaptchaValid = (ReCaptcha.Validate(EncodedResponse) == "True" ? true : false);
                if (IsCaptchaValid)
                {
                    if (contact.newsletter == true)
                    {
                        if (db.Subscribers.Where(s => s.Email.Equals(contact.emailAddress)).FirstOrDefault() == null)
                        {
                            Subscriber subscriber = new Subscriber();
                            subscriber.Email = contact.emailAddress;
                            db.Subscribers.Add(subscriber);
                            db.SaveChanges();
                        }
                    }

                    //send email
                    var body = "<p><b>Name:</b> {0}</p><p><b>Email address:</b> {1}</p><p><b>Contact number:</b> {2}</p><p><b>Enquiry:</b> {3}</p>";
                    string messageBody = string.Format(body, contact.customerName.ToString(), contact.emailAddress.ToString(), contact.contactNumber.ToString(), contact.message.ToString());

                    //send email
                    var message = new MailMessage();
                    message.Body = messageBody;
                    message.To.Add(new MailAddress("bethany.fowler14@gmail.com"));
                    message.From = new MailAddress("bobstyresandgarageservices@gmail.com");
                    message.Subject = "Bobs Tyres Online Enquiry";
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
                    return RedirectToAction("Contact", new { message = "Your message has been sent to Bobs Tyres" });
                }
            }
            return View(contact);
        }

    }
}