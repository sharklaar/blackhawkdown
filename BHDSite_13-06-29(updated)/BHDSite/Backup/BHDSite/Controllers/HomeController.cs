using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BHDSite.Library;
using Google.GData.Client;
using BHDSite.Models;
using Google.GData.Spreadsheets;

namespace BHD.Controllers
{
    [OutputCache(Duration = 604800, VaryByParam = "none")]
    public class HomeController : Controller
    {
        Helper _helper = new Helper();
        AtomEntryCollection worksheets;
        Home homePageContent = new Home();
        
        public ActionResult RemoveCache()
        {
            Helper.ForcedRefresh();

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            Response.AddCacheItemDependency("Pages");

            SetUpDriveConnection();

            GetHomePageContent();            
            return View(homePageContent);
        }



        private void SetUpDriveConnection()
        {
            worksheets = _helper.GetWorksheetFeed();
        }

        private void GetHomePageContent()
        {
            PageContent homePage = new PageContent();

            foreach (WorksheetEntry worksheet in worksheets)
            {
                if (worksheet.Title.Text == "home")
                {
                    homePage = _helper.GetPageContent(worksheet, "home");
                }
            }

            homePageContent.HomePageText = homePage.PageText;
            homePageContent.HomePageTitle = homePage.PageTitle;
        }
    }
}
