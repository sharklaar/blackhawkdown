using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhdResponsiveSite.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private readonly GoogleDriveHelper _driveHelper = new GoogleDriveHelper();

        public ActionResult Login(AccountModel account)
        {
            var loginValidated = _driveHelper.GetUserWithLoginCredentials(account);

            if (loginValidated)
            {
                AddCookie();
                return View("Members", account);
            }

            return View("FailedLogin");

        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(AccountModel account)
        {
            if (ModelState.IsValid)
            {
                _driveHelper.WriteNewUserToDatabase(account);
                AddCookie();
                return View("Members", account);
            }
            return View("Signup", account);

        }

        private void AddCookie()
        {
            var cookie = new HttpCookie("bhd_signin", "true");
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
        }
    }
}
