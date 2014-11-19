using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using log4net.Appender;
using Google.GData.Spreadsheets;
using Google.GData.Client;
using Docs = Google.GData.Documents;
using System.Threading;

namespace Log4Net.CP.GoogleSpreadSheetAppender
{
    public class GoogleSheetAppender : BufferingAppenderSkeleton
    {
        //Authentication
        public string Username { get; set; }
        public string Password { get; set; }

        public string SheetName { get; set; }

        private readonly Object _lock = new Object();

        public GoogleSheetAppender()
        {
        }

        protected override void SendBuffer(log4net.Core.LoggingEvent[] events)
        {
            ThreadPool.QueueUserWorkItem(t => _SendBuffer(events));
        }

        private void _SendBuffer(log4net.Core.LoggingEvent[] events)
        {
            try
            {
                lock (_lock)
                {
                    AddRows(events);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Error("Error occurred while add log events to google spreadsheets.", ex);
            }
        }

        private void AddRows(log4net.Core.LoggingEvent[] events)
        {
            SpreadsheetsService service = new SpreadsheetsService("Log4Net.GoogleSpreadSheetsAppender");

            service.setUserCredentials(Username, Password);

            // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.
            SpreadsheetQuery query = new SpreadsheetQuery()
            {
                Exact = false,
                Title = SheetName,
                StartDate = DateTime.Now.AddDays(-1)
            };

            // Make a request to the API and get all spreadsheets.
            SpreadsheetFeed feed = service.Query(query);
            var spreadsheetName = SheetName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            SpreadsheetEntry spreadsheet = EnsureSpreadSheet(service, feed, spreadsheetName);

            // Get the first worksheet of the first spreadsheet.
            WorksheetFeed wsFeed = spreadsheet.Worksheets;


            foreach (var evnt in events)
            {
                var level = evnt.Level.ToString();
                WorksheetEntry worksheet = EnsureWorksheet(service, wsFeed, level);
                // Define the URL to request the list feed of the worksheet.
                AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                // Create a local representation of the new row.
                ListEntry row = new ListEntry();
                row.Elements.Add(new ListEntry.Custom() { LocalName = "timestamp", Value = evnt.TimeStamp.ToString() });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "level", Value = evnt.Level.ToString() });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "message", Value = evnt.RenderedMessage });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "domain", Value = evnt.Domain });
                row.Elements.Add(new ListEntry.Custom() { LocalName = "username", Value = evnt.UserName });

                // Send the new row to the API for insertion.
                service.Insert(new Uri(listFeedLink.AbsoluteUri), row);
            }
        }

        private static WorksheetEntry EnsureWorksheet(SpreadsheetsService service, WorksheetFeed wsFeed, string name)
        {
            WorksheetEntry worksheet = null;
            if (wsFeed.Entries.Count > 0)
                foreach (var entry in wsFeed.Entries)
                {
                    if (((WorksheetEntry)entry).Title.Text == name)
                    {
                        worksheet = (WorksheetEntry)entry;
                        break;
                    }
                }
            if (worksheet == null)
            {
                worksheet = new WorksheetEntry();
                worksheet.Title.Text = name;
                worksheet.ColCount = new ColCountElement(5);
                worksheet.RowCount = new RowCountElement(2);
                worksheet.Cols = 5;
                worksheet.Rows = 2;

                worksheet = wsFeed.Insert(worksheet);
                worksheet.Update();
                //build header row
                // Fetch the cell feed of the worksheet.
                CellQuery cellQuery = new CellQuery(worksheet.CellFeedLink);
                CellFeed cellFeed = service.Query(cellQuery);
                CellEntry cellEntry = new CellEntry(1, 1, "timestamp");
                cellFeed.Insert(cellEntry);
                cellEntry = new CellEntry(1, 2, "level");
                cellFeed.Insert(cellEntry);
                cellEntry = new CellEntry(1, 3, "message");
                cellFeed.Insert(cellEntry);
                cellEntry = new CellEntry(1, 4, "domain");
                cellFeed.Insert(cellEntry);
                cellEntry = new CellEntry(1, 5, "username");
                cellFeed.Insert(cellEntry);
            }
            return worksheet;
        }

        private SpreadsheetEntry EnsureSpreadSheet(SpreadsheetsService service, SpreadsheetFeed feed, string name)
        {
            SpreadsheetEntry spreadsheet = null;
            if (feed.Entries.Count > 0)
                foreach (var entry in feed.Entries)
                {
                    if (((SpreadsheetEntry)entry).Title.Text == name)
                    {
                        spreadsheet = (SpreadsheetEntry)entry;
                        break;
                    }
                }
            if (spreadsheet != null)
                return spreadsheet;

            Docs.DocumentsService docservice = new Docs.DocumentsService("Log4Net.GoogleSpreadSheetsAppender");

            docservice.setUserCredentials(Username, Password);

            Docs.DocumentEntry newentry = new Docs.DocumentEntry();

            // Set the spreadsheet title
            newentry.Title.Text = name;

            // Add the spreadsheet category
            newentry.Categories.Add(Docs.DocumentEntry.SPREADSHEET_CATEGORY);

            // Make a request to the API and create the spreadsheet.
            docservice.Insert(new Uri(Docs.DocumentsListQuery.documentsBaseUri.Replace("http://", "https://")), newentry);

            // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.
            SpreadsheetQuery query = new SpreadsheetQuery()
            {
                Exact = false,
                Title = SheetName,
                StartDate = DateTime.Now.AddDays(-1)
            };

            // Make a request to the API and get all spreadsheets.
            SpreadsheetFeed newfeed = service.Query(query);

            if (newfeed.Entries.Count == 0)
            {
                throw new Exception("No spreadsheets found!");
            }
            if (newfeed.Entries.Count > 0)
                foreach (var entry in newfeed.Entries)
                {
                    if (((SpreadsheetEntry)entry).Title.Text == name)
                    {
                        spreadsheet = (SpreadsheetEntry)entry;
                        break;
                    }
                }
            return spreadsheet;
        }
    }
}
