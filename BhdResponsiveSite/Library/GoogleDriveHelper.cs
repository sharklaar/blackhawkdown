using BhdResponsiveSite.Models;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using System;

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

        private AtomEntryCollection GetWorksheetFeed(string workbookName)
        {
            _spreadsheetsService = GetSpreadsheetService();

            var query = new SpreadsheetQuery();

            var feed = _spreadsheetsService.Query(query);

            foreach (var entry in feed.Entries)
            {
                if (entry.Title.Text == workbookName)
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
            _worksheets = GetWorksheetFeed("contacts");

            var worksheet = _worksheets[0] as WorksheetEntry;

            if (worksheet == null) return;
            var listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            
            var listQuery = new ListQuery(listFeedLink.HRef.ToString());

            var listFeed = _spreadsheetsService.Query(listQuery); 
            
            var row = new ListEntry();

            row.Elements.Add(new ListEntry.Custom { LocalName = "email", Value = emailAddress });
            row.Elements.Add(new ListEntry.Custom { LocalName = "date", Value = DateTime.Now.ToShortDateString() });
            
            _spreadsheetsService.Insert(listFeed, row);
        }

        public void WriteNewUserToDatabase(AccountModel accountModel)
        {
            _worksheets = GetWorksheetFeed("accounts");

            var worksheet = _worksheets[0] as WorksheetEntry;

            if (worksheet == null) return;
            var listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            var listQuery = new ListQuery(listFeedLink.HRef.ToString());

            var listFeed = _spreadsheetsService.Query(listQuery);

            var row = new ListEntry();

            row.Elements.Add(new ListEntry.Custom { LocalName = "email", Value = accountModel.Email });
            row.Elements.Add(new ListEntry.Custom { LocalName = "firstname", Value = accountModel.FirstName });
            row.Elements.Add(new ListEntry.Custom { LocalName = "lastname", Value = accountModel.LastName });
            row.Elements.Add(new ListEntry.Custom { LocalName = "password", Value = accountModel.Password });
            row.Elements.Add(new ListEntry.Custom { LocalName = "username", Value = accountModel.Username });
            row.Elements.Add(new ListEntry.Custom { LocalName = "date", Value = DateTime.Now.ToShortDateString() });

            _spreadsheetsService.Insert(listFeed, row);
        }

        public bool GetUserWithLoginCredentials(AccountModel accountModel)
        {
            _worksheets = GetWorksheetFeed("accounts");

            var worksheet = _worksheets[0] as WorksheetEntry;

            if (worksheet == null) 
                return false;

            var cellQuery = new CellQuery(worksheet.CellFeedLink);

            var cellFeed = _spreadsheetsService.Query(cellQuery);

            var cellRow = GetCellRowForUser(cellFeed, accountModel);

            foreach (CellEntry cell in cellFeed.Entries)
            {
                if (cell.Row == cellRow && cell.Column == 4)
                {
                    return cell.Value == accountModel.Password;
                }
            }
            return false;
        }

        private int GetCellRowForUser(CellFeed cellFeed, AccountModel accountModel)
        {
            foreach (CellEntry cell in cellFeed.Entries)
            {
                if (cell.Column == 5)
                {
                    if (cell.Cell.Value == accountModel.Username)
                    {
                        return Convert.ToInt32(cell.Row);
                    }
                }
            }
            return 0;
        }
    }
}