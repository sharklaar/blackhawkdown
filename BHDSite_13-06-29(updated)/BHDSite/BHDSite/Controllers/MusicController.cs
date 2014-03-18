using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHDSite.Controllers
{
    public class MusicController : Controller
    {
        //
        // GET: /Music/

        public ActionResult Index()
        {
            ViewBag.Title = "BlackHawkDown - Our Music";
            return View();
        }

    }
}
