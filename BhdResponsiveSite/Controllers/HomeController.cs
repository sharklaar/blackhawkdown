using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BhdResponsiveSite.Library;

namespace BhdResponsiveSite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //var helper = new GoogleDriveHelper();
            //helper.WriteEmailToDatabase("marc@blackhawkdown.org.uk");
            
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
