
using System.Web.Mvc;
using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using BHDSite.Library;
using System.Text;
using System;
using Postal;

namespace BhdResponsiveSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(EmailOnlyModel emailVm)
        {
            if (!ModelState.IsValid)
            {
                return View(emailVm);
            }

            var driveHelper = new GoogleDriveHelper();
            driveHelper.WriteEmailToDatabase(emailVm.Email);

            dynamic email = new Postal.Email("FreeTracks");
            email.To = emailVm.Email;


            var messageBody = GetMessageBody();

            var contact = new Contact
            {
                From = emailVm.Email,
                Message = GetMessageBody(),
                Subject = "your free tracks"
            };

            new Library.Email().SendAutoResponse(contact);

            return View();
        }

        public ActionResult Gigs()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Links()
        {
            return View();
        }

        public ActionResult Music()
        {
            return View();
        }

        public ActionResult NextGig()
        {
            return View();
        }

        public ActionResult FreeStuff()
        {
            return View();
        }

        private string GetMessageBody()
        {
            var mailBody = new StringBuilder();
            mailBody.Append("Hey there rock fan!");
            mailBody.Append(Environment.NewLine);
            mailBody.Append(Environment.NewLine);
            mailBody.Append("Thanks for registering with BlackHawkDown.org.uk. In return, as promised, please find below your link to download THREE FREE TRACKS!");
            mailBody.Append(Environment.NewLine);
            mailBody.Append(Environment.NewLine);
            mailBody.Append("http://www.blackhawkdown.org.uk/freetrackdownload");
            mailBody.Append(Environment.NewLine);
            mailBody.Append(Environment.NewLine);
            mailBody.Append("Thanks again, and drop in on the site from time to time, we'll be adding some new tracks to our videos page soon.");
            mailBody.Append(Environment.NewLine);
            mailBody.Append(Environment.NewLine);
            mailBody.Append("Cheers");
            mailBody.Append(Environment.NewLine);
            mailBody.Append("BlackHawkDown");

            return mailBody.ToString();
        }
    }
}
