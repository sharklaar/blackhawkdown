using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using BHDSite.Library;
using System.Text;
using System.Web.Caching;
using System.Collections;
using BHDSite.Models;


namespace BHD.Controllers
{
    public class MembersController : Controller
    {
        Helper _helper = new Helper();
        AtomEntryCollection worksheets;
        Member memberPageContent = new Member();

        public ActionResult RemoveCache()
        {
            Helper.ForcedRefresh();

            return RedirectToAction("Index");
        }
        //
        // GET: /Members/
        [OutputCache(Duration = 604800, VaryByParam = "none")]
        public ActionResult Index()
        {
            Response.AddCacheItemDependency("Pages");

            SetUpDriveConnection();

            GetBandPageContent();            
            return View(memberPageContent);
        }

        private void SetUpDriveConnection()
        {
            worksheets = _helper.GetWorksheetFeed();
        }

        private void GetBandPageContent()
        {
            PageContent benContent = new PageContent();
            PageContent norbContent = new PageContent();
            PageContent andyContent = new PageContent();
            PageContent hemyContent = new PageContent();
            PageContent sharkContent = new PageContent();
            PageContent bandContent = new PageContent();

            foreach (WorksheetEntry worksheet in worksheets)
            {
                if (worksheet.Title.Text == "ben")
                {
                    benContent = _helper.GetPageContent(worksheet, "ben");
                }
                if (worksheet.Title.Text == "norb")
                {
                    norbContent = _helper.GetPageContent(worksheet, "norb");
                }
                if (worksheet.Title.Text == "andy")
                {
                    andyContent = _helper.GetPageContent(worksheet, "Andy");
                }
                if (worksheet.Title.Text == "hemy")
                {
                    hemyContent = _helper.GetPageContent(worksheet, "hemy");
                }
                if (worksheet.Title.Text == "shark")
                {
                    sharkContent = _helper.GetPageContent(worksheet, "shark");
                }
                if (worksheet.Title.Text == "band")
                {
                    bandContent = _helper.GetPageContent(worksheet, "band");
                }
            }
            memberPageContent.BenTitle = benContent.PageTitle;
            memberPageContent.BenText = benContent.PageText;
            memberPageContent.NorbTitle = norbContent.PageTitle;
            memberPageContent.NorbText = norbContent.PageText;
            memberPageContent.AndyTitle = andyContent.PageTitle;
            memberPageContent.AndyText = andyContent.PageText;
            memberPageContent.HemyTitle = hemyContent.PageTitle;
            memberPageContent.HemyText = hemyContent.PageText;
            memberPageContent.SharkTitle = sharkContent.PageTitle;
            memberPageContent.SharkText = sharkContent.PageText;
            memberPageContent.BandTitle = bandContent.PageTitle;
            memberPageContent.BandText = bandContent.PageText;
        }
    }
}
