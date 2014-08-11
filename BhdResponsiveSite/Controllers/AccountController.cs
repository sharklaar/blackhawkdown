using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using System;
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
            var validatedAccount = _driveHelper.GetUserWithLoginCredentials(account);

            if (validatedAccount != null)
            {
                AddCookie();
                Session["login"] = validatedAccount;

                return View("Members", validatedAccount);                

            }

            return View("FailedLogin");

        }

        public ActionResult Members()
        {
            if (Session["login"] != null)
                return View();

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

            var cookie = new HttpCookie("bhd_login", "true") {Expires = DateTime.Now.AddYears(1)};
            Response.Cookies.Add(cookie);
        }
    }
}
