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

        public ActionResult Login(AccountModel account)
        {

            return View("Members", account);
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount()
        {
            return View();
        }

    }
}
