using Google.GData.Client;
using Google.GData.Spreadsheets;
using System.Linq;

namespace BhdResponsiveSite.Library
{
    public class GoogleDriveHelper
    {
        private SpreadsheetsService _spreadsheetsService;
        private AtomEntryCollection _worksheets;
        private AtomLink _link;

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

            foreach (var entry in feed.Entries)
            {
                if (entry.Title.Text == "contacts")
                {
                    _link = entry.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

                    WorksheetQuery worksheetQuery = new WorksheetQuery(_link.HRef.ToString());
                    WorksheetFeed worksheetFeed = _spreadsheetsService.Query(worksheetQuery);

                    return worksheetFeed.Entries;
                }
            }
            return null;
        }

        public void WriteEmailToDatabase(string emailAddress)
        {
            _worksheets = GetWorksheetFeed();

            var worksheet = _worksheets[0] as WorksheetEntry;

            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());

            ListFeed listFeed = _spreadsheetsService.Query(listQuery); 
            
            ListEntry row = new ListEntry();

            row.Elements.Add(new ListEntry.Custom() { LocalName = "email", Value = emailAddress }); 
            
            _spreadsheetsService.Insert(listFeed, row);

        }
    }
}