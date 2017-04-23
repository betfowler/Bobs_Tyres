using Bobs_Tyres.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
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
            ViewBag.Success = message;
            return View();
        }

        public async Task<bool> SendMail(string messageBody, string to)
        {
            //send email
            var message = new MailMessage();
            message.Body = messageBody;
            message.To.Add(new MailAddress(to));
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
            return true;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact([Bind(Include = "customerName,emailAddress,contactNumber,message,newsletter")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                string EncodedResponse = Request["g-recaptcha-response"];
                var client = new WebClient();
                string PrivateKey = "6LeHVh0UAAAAAFHTfzxLL8LSFhmT27ZawwG9Oxbb";
                var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", PrivateKey, EncodedResponse));
                JObject jobject = JObject.Parse(reply);
                var captchaResponse = (bool)jobject["success"];

                if(captchaResponse != true)
                {
                    var errormessages = jobject["error-codes"].ToArray();
                    foreach(var m in errormessages)
                    {
                        if (m.ToString() == "missing-input-secret" || m.ToString() == "invalid-input-secret")
                        {
                            ViewBag.CaptchaErrorMessage = "Administrator error.  Error has been reported.";

                            //send email
                            var errorbody = "<h1>Bobs Tyres Ltd ReCaptcha Error</h1><h3>The following recaptcha error has occured: <b>{0}</b>";
                            string errorMessage = string.Format(errorbody, m.ToString());
                            var adminTo = "bethany.fowler14@gmail.com";

                            if (await SendMail(errorMessage, adminTo))
                            {
                                return View();
                            }

                        }
                        else
                        {
                            ViewBag.CaptchaErrorMessage = "An error occured.  Please try again ensuring that the security box is ticked.";
                            return View();
                        }
                    }
                }

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
                var to = "bethany.fowler14@gmail.com"; //change to bobs tyres address

                if(await SendMail(messageBody, to))
                {
                    return RedirectToAction("Contact", new { message = "Your message has been sent to Bobs Tyres" });
                }
            }
            return View();
        }
    }
}