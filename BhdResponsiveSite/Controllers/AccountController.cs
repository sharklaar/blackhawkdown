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
            _driveHelper.WriteNewUserToDatabase(account);

            return View();
        }
    }
}
