using System.IO;
using System.Text;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Code
{
    public class ReportPrintDocument : PrintDocument
    {
        private readonly PageSettings _mPageSettings;
        private int _mCurrentPage;
        private readonly List<Stream> _mPages = new List<Stream>();

        public ReportPrintDocument(ServerReport serverReport)
            : this((Report)serverReport)
        {
            RenderAllServerReportPages(serverReport);
        }

        public ReportPrintDocument(LocalReport localReport)
            : this((Report)localReport)
        {
            RenderAllLocalReportPages(localReport);
        }

        private ReportPrintDocument(Report report)
        {
            // Set the page settings to the default defined in the report
            ReportPageSettings reportPageSettings = report.GetDefaultPageSettings();

            // The page settings object will use the default printer unless
            // PageSettings.PrinterSettings is changed.  This assumes there
            // is a default printer.
            _mPageSettings = new PageSettings
            {
                Landscape = true,
                PaperSize = reportPageSettings.PaperSize,
                Margins = reportPageSettings.Margins
            };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                foreach (Stream s in _mPages)
                {
                    s.Dispose();
                }

                _mPages.Clear();
            }
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);

            _mCurrentPage = 0;
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);

            Stream pageToPrint = _mPages[_mCurrentPage];
            pageToPrint.Position = 0;

            // Load each page into a Metafile to draw it.
            using (Metafile pageMetaFile = new Metafile(pageToPrint))
            {
                Rectangle adjustedRect = new Rectangle(
                        e.PageBounds.Left - (int)e.PageSettings.HardMarginX,
                        e.PageBounds.Top - (int)e.PageSettings.HardMarginY,
                        e.PageBounds.Width,
                        e.PageBounds.Height);

                // Draw a white background for the report
                e.Graphics.FillRectangle(Brushes.White, adjustedRect);

                // Draw the report content
                e.Graphics.DrawImage(pageMetaFile, adjustedRect);

                // Prepare for next page.  Make sure we haven't hit the end.
                _mCurrentPage++;
                e.HasMorePages = _mCurrentPage < _mPages.Count;
            }
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            e.PageSettings = (PageSettings)_mPageSettings.Clone();
        }

        private void RenderAllServerReportPages(ServerReport serverReport)
        {
            string deviceInfo = CreateEmfDeviceInfo();

            // Generating Image renderer pages one at a time can be expensive.  In order
            // to generate page 2, the server would need to recalculate page 1 and throw it
            // away.  Using PersistStreams causes the server to generate all the pages in
            // the background but return as soon as page 1 is complete.
            NameValueCollection firstPageParameters = new NameValueCollection { { "rs:PersistStreams", "True" } };

            // GetNextStream returns the next page in the sequence from the background process
            // started by PersistStreams.
            NameValueCollection nonFirstPageParameters = new NameValueCollection { { "rs:GetNextStream", "True" } };

            string mimeType;
            string fileExtension;
            Stream pageStream = serverReport.Render("IMAGE", deviceInfo, firstPageParameters, out mimeType, out fileExtension);

            // The server returns an empty stream when moving beyond the last page.
            while (pageStream.Length > 0)
            {
                _mPages.Add(pageStream);

                pageStream = serverReport.Render("IMAGE", deviceInfo, nonFirstPageParameters, out mimeType, out fileExtension);
            }
        }

        private void RenderAllLocalReportPages(LocalReport localReport)
        {
            string deviceInfo = CreateEmfDeviceInfo();

            Warning[] warnings;
            localReport.Render("IMAGE", deviceInfo, LocalReportCreateStreamCallback, out warnings);
        }

        private Stream LocalReportCreateStreamCallback(
            string name,
            string extension,
            Encoding encoding,
            string mimeType,
            bool willSeek)
        {
            MemoryStream stream = new MemoryStream();
            _mPages.Add(stream);

            return stream;
        }

        private string CreateEmfDeviceInfo()
        {
            PaperSize paperSize = _mPageSettings.PaperSize;
            Margins margins = _mPageSettings.Margins;

            // The device info string defines the page range to print as well as the size of the page.
            // A start and end page of 0 means generate all pages.
            return string.Format(
                CultureInfo.InvariantCulture,
                "<DeviceInfo><OutputFormat>emf</OutputFormat><StartPage>0</StartPage><EndPage>0</EndPage><MarginTop>{0}</MarginTop><MarginLeft>{1}</MarginLeft><MarginRight>{2}</MarginRight><MarginBottom>{3}</MarginBottom><PageHeight>8.5in</PageHeight>11in<PageWidth></PageWidth></DeviceInfo>",
                ToInches(margins.Top),
                ToInches(margins.Left),
                ToInches(margins.Right),
                ToInches(margins.Bottom),
                ToInches(paperSize.Height),
                ToInches(paperSize.Width));
        }

        private static string ToInches(int hundrethsOfInch)
        {
            double inches = hundrethsOfInch / 100.0;
            return inches.ToString(CultureInfo.InvariantCulture) + "in";
        }



        //private int m_currentPageIndex;
        //private IList<Stream> m_streams;

        //// Routine to provide to the report renderer, in order to
        ////    save an image for each page of the report.
        //private Stream CreateStream(string name,
        //  string fileNameExtension, Encoding encoding,
        //  string mimeType, bool willSeek)
        //{
        //    Stream stream = new MemoryStream();
        //    m_streams.Add(stream);
        //    return stream;
        //}

        //// Export the given report as an EMF (Enhanced Metafile) file.
        //public void Export(LocalReport report)
        //{
        //    string deviceInfo =
        //      @"<DeviceInfo>
        //        <OutputFormat>EMF</OutputFormat>
        //        <PageWidth>8.5in</PageWidth>
        //        <PageHeight>11in</PageHeight>
        //        <MarginTop>0.25in</MarginTop>
        //        <MarginLeft>0.25in</MarginLeft>
        //        <MarginRight>0.25in</MarginRight>
        //        <MarginBottom>0.25in</MarginBottom>
        //    </DeviceInfo>";
        //    Warning[] warnings;
        //    m_streams = new List<Stream>();
        //    report.Render("Image", deviceInfo, CreateStream,
        //       out warnings);
        //    foreach (Stream stream in m_streams)
        //        stream.Position = 0;
        //}

        //private static string ToInches(int hundrethsOfInch)
        //{
        //    double inches = hundrethsOfInch / 100.0;
        //    return inches.ToString(CultureInfo.InvariantCulture) + "in";
        //}

        //// Handler for PrintPageEvents
        //private void PrintPage(object sender, PrintPageEventArgs ev)
        //{
        //    Metafile pageImage = new
        //       Metafile(m_streams[m_currentPageIndex]);

        //    // Adjust rectangular area with printer margins.
        //    Rectangle adjustedRect = new Rectangle(
        //        ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
        //        ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
        //        ev.PageBounds.Width,
        //        ev.PageBounds.Height);

        //    // Draw a white background for the report
        //    ev.Graphics.FillRectangle(Brushes.White, adjustedRect);

        //    // Draw the report content
        //    ev.Graphics.DrawImage(pageImage, adjustedRect);

        //    // Prepare for the next page. Make sure we haven't hit the end.
        //    m_currentPageIndex++;
        //    ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        //}

        //public void Print()
        //{
        //    if (m_streams == null || m_streams.Count == 0)
        //        throw new Exception("Error: no stream to print.");
        //    PrintDocument printDoc = new PrintDocument();
        //    if (!printDoc.PrinterSettings.IsValid)
        //    {
        //        throw new Exception("Error: cannot find the default printer.");
        //    }
        //    else
        //    {
        //        printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
        //        m_currentPageIndex = 0;
        //        printDoc.Print();
        //    }
        //}
    }
}