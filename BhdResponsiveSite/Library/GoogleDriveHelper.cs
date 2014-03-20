using Google.GData.Client;
using Google.GData.Spreadsheets;

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

                    var worksheetQuery = new WorksheetQuery(_link.HRef.ToString());
                    var worksheetFeed = _spreadsheetsService.Query(worksheetQuery);

                    return worksheetFeed.Entries;
                }
            }
            return null;
        }

        public void WriteEmailToDatabase(string emailAddress)
        {
            _worksheets = GetWorksheetFeed();

            var worksheet = _worksheets[0] as WorksheetEntry;

            if (worksheet == null) return;
            var listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            
            var listQuery = new ListQuery(listFeedLink.HRef.ToString());

            var listFeed = _spreadsheetsService.Query(listQuery); 
            
            var row = new ListEntry();

            row.Elements.Add(new ListEntry.Custom { LocalName = "email", Value = emailAddress }); 
            
            _spreadsheetsService.Insert(listFeed, row);
        }
    }
}