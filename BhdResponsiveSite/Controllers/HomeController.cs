using System.Linq;
using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web;
using BhdResponsiveSite.Helpers;

namespace BhdResponsiveSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var gigList = new DatabaseHelper().GetGigDetails().OrderBy(x => x.GigDate).ToList();

            return View(new GigList{ Gigs = gigList});
        }
        
        [ChildActionOnly]
        public ActionResult Email()
        {
            if (Request.Cookies["bhd_subscribe"] != null)
            {
                return null;
            }

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
                Console.WriteLine(ex);
                return PartialView("_email", emailVm);
            }
            
        }

        public ActionResult FreeTracks()
        {
            AddCookie();
            var freeTrackCode = new DatabaseHelper().GetFreeTrackCode();
            var url = $"https://blackhawkdownuk.bandcamp.com/yum?code={freeTrackCode}";
            return View(new FreeTrack {Url = url});
        }

        public ActionResult Gigs()
        {
            var gigList = new DatabaseHelper().GetGigDetails().OrderBy(x => x.GigDate).ToList();
            return View(gigList);
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
            ViewBag.OgImage = "http://img.youtube.com/vi/gpoXAWDQd84/0.jpg";
            return View(ViewBag);
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
            var gigList = new DatabaseHelper().GetGigDetails().OrderBy(x => x.GigDate).ToList();

            return View(model: new GigList { Gigs = gigList });
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
