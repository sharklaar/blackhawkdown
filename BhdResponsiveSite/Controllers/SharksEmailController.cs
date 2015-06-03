using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BhdResponsiveSite.Helpers;
using BhdResponsiveSite.Models;

namespace BhdResponsiveSite.Controllers
{
    public class SharksEmailController : Controller
    {
        //
        // GET: /SharksEmail/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmails(EmailSendingModel emailSendingmodel)
        {
            var emails = GetEmailList();
            emailSendingmodel.EmailList = emails;
            
            return null;
        }

        private List<string> GetEmailList()
        {
            var databaseHelper = new DatabaseHelper();
            var emails = databaseHelper.GetEmailAddresses();

            return emails;
        } 
    }
}
