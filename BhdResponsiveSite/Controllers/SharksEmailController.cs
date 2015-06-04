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

            var emailHelper = new Library.Email();

            var resultsList = new List<string>();

            foreach (var email in emailSendingmodel.EmailList)
            {
                var emailModel = new EmailOnlyModel { Email = email };

                var result = "success";//emailHelper.SendAutoResponse(emailModel, emailSendingmodel.EmailType);

                resultsList.Add(result);
            }

            return View("EmailsSent");
        }

        private List<string> GetEmailList()
        {
            var databaseHelper = new DatabaseHelper();
            var emails = databaseHelper.GetEmailAddresses();

            return emails;
        } 
    }
}
