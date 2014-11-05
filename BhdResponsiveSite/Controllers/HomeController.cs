using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using System;
using System.Web.Mvc;
using System.Web;

namespace BhdResponsiveSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [ChildActionOnly]
        public ActionResult Email()
        {
            if (Request.Cookies["bhd_subscribe"] != null)
                return null;

            return PartialView("_email");
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult Email(EmailOnlyModel emailVm)
        {
            if (Request.Cookies["bhd_subscribe"] != null)
                return null;

            if (!ModelState.IsValid)
            {
                return PartialView("_email", emailVm);
            }

            WriteEmailToDatabase(emailVm);
            try
            {
                AddCookie();

                SendAutoResponse(emailVm);

                return null;
            }
            catch (Exception ex)
            {
                return PartialView("_email", emailVm);
            }            
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

        public ActionResult Demo()
        {
            return View();
        }

        private void WriteEmailToDatabase(EmailOnlyModel emailVm)
        {
            var driveHelper = new GoogleDriveHelper();
            driveHelper.WriteEmailToDatabase(emailVm.Email);
        }

        private void AddCookie()
        {
            var cookie = new HttpCookie("bhd_subscribe", "true");
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
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
