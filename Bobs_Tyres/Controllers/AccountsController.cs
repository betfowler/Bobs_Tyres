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

        public ActionResult Newsletter()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Newsletter([Bind(Include = "Message")] Newsletter newsletter)
        {
            string fbAppId = "646957715495504";
            string fbSecretAppId = "c77c80685db16953cb93e075bc25c302";
            var accessToken = "EAACEdEose0cBAENrZCRbN5DPUOhrhG5UbquZB6ZCSMputZBUToZCr7bchD4gU7xdlR9qgkKJvm0ZAwg4N3H8ZBnTn51FNxOlVZBLnnCjAx3oYtmbmu2TBsokVcYYMBZCUhP015EhYc9oZBzcw56FZAOU02YI1am1FUjpqS7ZCrlvHtkmmZA5jwvgZB3fMaN6yuZBHF3ZCAFRIDJiKOuEjQZDZD";
            
            return View();
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
