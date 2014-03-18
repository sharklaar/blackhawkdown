using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDSite.Models;
using BHDSite.Library;

namespace BHD.Controllers
{
    public class ContactController : Controller
    {
        public ActionResult RemoveCache()
        {
            Helper.ForcedRefresh();

            return RedirectToAction("Index");
        }
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            Response.AddCacheItemDependency("Pages");

            ViewBag.Message = "Contact Us";            
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactViewModel contactVM)
        {
            if (!ModelState.IsValid)
            {
                return View(contactVM);
            }

            var contact = new Contact
            {
                From = contactVM.From,
                Subject = contactVM.Subject,
                Message = contactVM.Message
            };

            new Email().Send(contact);

            return RedirectToAction("ContactConfirm");
        }

        public ActionResult Contact()
        {
            return View(new ContactViewModel());
        }

        public ActionResult ContactConfirm()
        {
            return View();
        }
    }
}
