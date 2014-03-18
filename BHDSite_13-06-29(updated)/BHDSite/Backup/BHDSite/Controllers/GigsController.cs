using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDSite.Library;
using BHDSite.Models;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace BHD.Controllers
{
    public class GigsController : Controller
    {
        Helper _helper = new Helper();
        AtomEntryCollection worksheets;
        Gig gigsPageContent = new Gig();

        public ActionResult RemoveCache()
        {
            Helper.ForcedRefresh();

            return RedirectToAction("Index");
        }
        //
        // GET: /Gigs/
        [OutputCache(Duration = 604800, VaryByParam = "none")]
        public ActionResult Index()
        {
            Response.AddCacheItemDependency("Pages");

            SetUpDriveConnection();

            GetGigsPageContent();            
            return View(gigsPageContent);
        }

        private void SetUpDriveConnection()
        {
            worksheets = _helper.GetWorksheetFeed();
        }

        private void GetGigsPageContent()
        {
            PageContent gigsPage = new PageContent();

            foreach (WorksheetEntry worksheet in worksheets)
            {
                if (worksheet.Title.Text == "gigs")
                {
                    gigsPage = _helper.GetPageContent(worksheet, "gigs");
                }
            }

            gigsPageContent.GigPageText = gigsPage.PageText;
            gigsPageContent.GigPageTitle = gigsPage.PageTitle;
        }
    }
}
