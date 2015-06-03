using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using System;
using System.Web.Mvc;
using System.Web;
using BhdResponsiveSite.Helpers;

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
            var emailHelper = new Email();

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

                emailHelper.SendAutoResponse(emailVm, "FreeTracks");
                emailVm.JustSentEmail = true;
                return PartialView("_email", emailVm);
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

        public ActionResult PressKit()
        {
            return View();
        }

        public FileResult Download(string file)
        {
            var filename = Server.MapPath("~/Content/press-kit/images/") + file;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filename);
            var response = new FileContentResult(fileBytes, "application/octet-stream");
            response.FileDownloadName = filename;
            return response;
        }

        private void WriteEmailToDatabase(EmailOnlyModel emailVm)
        {
            var driveHelper = new DatabaseHelper();
            driveHelper.WriteEmailToDatabase(emailVm);
        }

        private void AddCookie()
        {
            var cookie = new HttpCookie("bhd_subscribe", "true");
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
        }
    }
}
