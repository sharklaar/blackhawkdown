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
            var emails = new List<string>();

            if (emailSendingmodel.IsTestEmail)
            {
                emails.Add("marc@blackhawkdown.org.uk");
            }
            else
            {
                //emails = GetEmailList();
            }

            emailSendingmodel.EmailList = emails;

            var emailHelper = new Library.Email();

            var sendingResults = new EmailSendingResultsModel();

            foreach (var email in emailSendingmodel.EmailList)
            {
                var emailModel = new EmailOnlyModel { Email = email };

                var result = emailHelper.SendAutoResponse(emailModel, emailSendingmodel.EmailType);

                if (result.Contains("success"))
                {
                    sendingResults.Successes++;
                }
                else
                {
                    sendingResults.Failures++;
                }

            }

            return View("EmailsSent", sendingResults);
        }

        private List<string> GetEmailList()
        {
            var databaseHelper = new DatabaseHelper();
            var emails = databaseHelper.GetEmailAddresses();

            return emails;
        } 
    }
}
