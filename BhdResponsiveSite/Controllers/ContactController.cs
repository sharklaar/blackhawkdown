using System.Web.Mvc;
using BhdResponsiveSite.Library;
using BhdResponsiveSite.Models;
using BHDSite.Library;

namespace BhdResponsiveSite.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            Response.AddCacheItemDependency("Pages");

            ViewBag.Message = "Contact Us";
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contactVm)
        {
            if (!ModelState.IsValid)
            {
                return null;
                //return View(contactVm);
            }

            var contact = new Contact
            {
                From = contactVm.Email,
                Subject = contactVm.Subject,
                Message = contactVm.Message
            };

            new Email().Send(contact);

            return RedirectToAction("ContactConfirm");
        }

        public ActionResult ContactConfirm()
        {
            return View();
        }
    }
}
