using Google.GData.Client;
using Google.GData.Spreadsheets;
using System.Linq;

namespace BhdResponsiveSite.Library
{
    public class GoogleDriveHelper
    {
        private SpreadsheetsService _spreadsheetsService;
        private AtomEntryCollection _worksheets;

        private SpreadsheetsService GetSpreadsheetService()
        {
            var myService = new SpreadsheetsService("AIzaSyCY05yDOV59mi24cPMwHzAg0Bw6vlQ9YK0");
            myService.setUserCredentials("websiteadmin@blackhawkdown.org.uk", "kickintheface");

            return myService;
        }

        private AtomEntryCollection GetWorksheetFeed()
        {
            _spreadsheetsService = GetSpreadsheetService();

            var query = new SpreadsheetQuery();

            var feed = _spreadsheetsService.Query(query);

            return (from SpreadsheetEntry entry in feed.Entries 
                    where entry.Title.Text == "contacts" 
                    select entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null) 
                    into link 
                    select new WorksheetQuery(link.HRef.ToString()) 
                    into wsQuery 
                    select _spreadsheetsService.Query(wsQuery) 
                    into wsFeed 
                    select wsFeed.Entries).FirstOrDefault();
        }

        public void WriteEmailToDatabase(string emailAddress)
        {
            _worksheets = GetWorksheetFeed();

            var worksheet = _worksheets[0] as WorksheetEntry;

            if (worksheet != null)
            {
                var cellQuery = new CellQuery(worksheet.CellFeedLink);
                var cellFeed = _spreadsheetsService.Query(cellQuery);

                foreach (var cell in cellFeed.Entries.Cast<CellEntry>().Where(cell => string.IsNullOrEmpty(cell.Value)))
                {
                    cell.InputValue = emailAddress;
                    cell.Update();
                }
            }
        }
    }
}