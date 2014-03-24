using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using System;
using System.Web.Mvc;

namespace BhdResponsiveSite.Controllers
{
    public class HomeController : Controller
    {
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

            WriteEmailToDatabase(emailVm);

            SendAutoResponse(emailVm);
            
            return View();
        }

        public ActionResult FreeTracks()
        {
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

        private void WriteEmailToDatabase(EmailOnlyModel emailVm)
        {
            var driveHelper = new GoogleDriveHelper();
            driveHelper.WriteEmailToDatabase(emailVm.Email);
        }

        private void SendAutoResponse(EmailOnlyModel emailVm)
        {
            var emailHelper = new Email();
            var client = emailHelper.GetClient();

            dynamic email = new Postal.Email("FreeTracks");
            email.To = emailVm.Email;
            var service = Postal.Email.CreateEmailService();
            var mailToSend = service.CreateMailMessage(email);
            
            try
            {
                client.Send(mailToSend);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
