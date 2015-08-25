using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhdResponsiveSite.Controllers
{
    public class StoreController : Controller
    {
        //
        // GET: /Store/

        public ActionResult Index()
        {
            ViewBag.OgSiteName = "BlackHawkDown Merch Store - buy the new EP here!";
            ViewBag.OgImage = "http://www.blackhawkdown.org.uk/Content/images/store/prefix.png";
            return View(ViewBag);
        }

        public ActionResult Thanks()
        {
            ViewBag.OgSiteName = "BlackHawkDown Merch Store - buy the new EP here!";
            return View(ViewBag);
        }
    }
}
