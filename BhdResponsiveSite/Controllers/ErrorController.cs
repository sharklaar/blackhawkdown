using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhdResponsiveSite.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult NotFound()
        {
            return View("NotFound");
        }

        public ActionResult ServerError()
        {
            return View("ServerError");
        }
    }
}
