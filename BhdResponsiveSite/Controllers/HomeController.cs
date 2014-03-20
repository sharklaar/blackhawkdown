
using System.Web.Mvc;
using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using BHDSite.Library;

namespace BhdResponsiveSite.Controllers
{
    public class HomeController : Controller
    {
        //
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

            var contact = new Contact
            {
                From = emailVm.Email,
                Message = "",
                Subject = "your free tracks"
            };

            new Email().SendAutoResponse(contact);

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
    }
}
