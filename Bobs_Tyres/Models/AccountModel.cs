using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bobs_Tyres.Models;
using Bobs_Tyres.Security;

namespace Bobs_Tyres.Models
{
    public class AccountModel
    {
        private Bobs_TyresContext db = new Bobs_TyresContext();

        public AccountModel()
        {

        }

        public Account find(string username)
        {
            return db.Accounts.Where(acc => acc.Username.Equals(username)).FirstOrDefault();
        }

        public bool login(string username, string password)
        {
            var getPassword = db.Accounts.SingleOrDefault(acc => acc.Username.Equals(username));
            return Hashing.ValidatePassword(password, getPassword.Password);
        }
    }
}