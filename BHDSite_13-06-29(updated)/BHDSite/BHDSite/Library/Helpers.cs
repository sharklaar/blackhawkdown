using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using BHDSite.Library;
using System.Text;
using System.Net;
using BHDSite.Models;

namespace BHDSite.Library
{
    public class Helper
    {
        SpreadsheetsService myService;
        Member siteContent = new Member();
        AtomEntryCollection worksheets; 

        private SpreadsheetsService GetService()
        {
            SpreadsheetsService myService = new SpreadsheetsService("AIzaSyCY05yDOV59mi24cPMwHzAg0Bw6vlQ9YK0");
            myService.setUserCredentials("websiteadmin@blackhawkdown.org.uk", "kickintheface");

            return myService;
        }

        public static void ForcedRefresh()
        {
            HttpContext.Current.Cache.Insert("Pages", DateTime.Now, null,
                  System.DateTime.MaxValue, System.TimeSpan.Zero,
                  System.Web.Caching.CacheItemPriority.NotRemovable,
                  null);
        }

        public AtomEntryCollection GetWorksheetFeed()
        {
            myService = GetService();

            SpreadsheetQuery query = new SpreadsheetQuery();

            SpreadsheetFeed feed = myService.Query(query);

            foreach (SpreadsheetEntry entry in feed.Entries)
            {
                if (entry.Title.Text == "WebsiteContent")
                {
                    AtomLink link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    WorksheetQuery wsQuery = new WorksheetQuery(link.HRef.ToString());
                    WorksheetFeed wsFeed = myService.Query(wsQuery);

                    return wsFeed.Entries;
                }
            }
            return null;
        }

        private void GetHome()
        {
            foreach (WorksheetEntry worksheet in worksheets)
            {
                if (worksheet.Title.Text == "home")
                {
                    PageContent homeContent = GetPageContent(worksheet, "home");
                }
            }
        }

        public PageContent GetPageContent(WorksheetEntry worksheet, string page)
        {
            PageContent result = new PageContent();
            AtomLink cellFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.CellRel, null);

            CellQuery cellQuery = new CellQuery(cellFeedLink.HRef.ToString());
            CellFeed cellFeed = myService.Query(cellQuery);

            StringBuilder sb = new StringBuilder();

            result.PageTitle = GetPageTitle(result, cellFeed);
                       
            if (page == "gigs")
            {
                sb.Append("<ul>");
            }

            sb = GetPageText(result, cellFeed, page, sb);

            if (page == "gigs")
            {
                sb.Append("</ul>");
            }

            result.PageText = sb.ToString();

            return result;
        }

        private static StringBuilder GetPageText(PageContent result, CellFeed cellFeed, string page, StringBuilder sb)
        {
            foreach (CellEntry currentCell in cellFeed.Entries)
            {
                if (currentCell.Title.Text == "B1")
                {
                }
                else if (currentCell.Title.Text == "A1")
                {
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentCell.Cell.Value))
                    {
                        if (page == "gigs")
                        {
                            sb.Append(string.Concat("<li>", currentCell.Cell.Value, "</li>"));
                        }
                        else
                        {
                            sb.Append(string.Concat("<p>", currentCell.Cell.Value, "</p>"));
                        }
                    }
                }
            }

            return sb;
        }

        private string GetPageTitle(PageContent result, CellFeed cellFeed)
        {
            foreach (CellEntry currentCell in cellFeed.Entries)
            {
                if (currentCell.Title.Text == "B1")
                {
                    string pageTitle = currentCell.Cell.Value;
                    return pageTitle;
                }
            }

            return string.Empty;
        }
    }
}