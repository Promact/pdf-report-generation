using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;

// Create a new PDF document
PdfDocument document = new PdfDocument();
document.Info.Title = "Invoice";

// Create an empty page
PdfPage page = document.AddPage();
XGraphics gfx = XGraphics.FromPdfPage(page);
XFont fontTitle = new XFont("Verdana", 20, XFontStyleEx.Bold);
XFont fontSubTitle = new XFont("Verdana", 14, XFontStyleEx.Bold);
XFont fontRegular = new XFont("Verdana", 12, XFontStyleEx.Regular);

// Draw the title
gfx.DrawString("INVOICE", fontTitle, XBrushes.Black, new XRect(0, 40, page.Width.Point, 40), XStringFormats.TopCenter);

// Draw invoice details
gfx.DrawString("Invoice Number: 12345", fontRegular, XBrushes.Black, new XRect(40, 100, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Invoice Date: " + DateTime.Now.ToShortDateString(), fontRegular, XBrushes.Black, new XRect(40, 120, page.Width.Point, 40), XStringFormats.TopLeft);

// Draw seller details
gfx.DrawString("Seller:", fontSubTitle, XBrushes.Black, new XRect(40, 160, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Company Name", fontRegular, XBrushes.Black, new XRect(40, 180, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Address Line 1", fontRegular, XBrushes.Black, new XRect(40, 200, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Address Line 2", fontRegular, XBrushes.Black, new XRect(40, 220, page.Width.Point, 40), XStringFormats.TopLeft);

// Draw buyer details
gfx.DrawString("Buyer:", fontSubTitle, XBrushes.Black, new XRect(40, 260, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Customer Name", fontRegular, XBrushes.Black, new XRect(40, 280, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Customer Address Line 1", fontRegular, XBrushes.Black, new XRect(40, 300, page.Width.Point, 40), XStringFormats.TopLeft);
gfx.DrawString("Customer Address Line 2", fontRegular, XBrushes.Black, new XRect(40, 320, page.Width.Point, 40), XStringFormats.TopLeft);

// Draw the table headers
gfx.DrawString("Description", fontSubTitle, XBrushes.Black, new XRect(40, 360, page.Width.Point / 2, 40), XStringFormats.TopLeft);
gfx.DrawString("Amount", fontSubTitle, XBrushes.Black, new XRect(page.Width.Point / 2 + 40, 360, page.Width.Point / 2, 40), XStringFormats.TopLeft);

// Draw the table content
gfx.DrawString("Item 1", fontRegular, XBrushes.Black, new XRect(40, 380, page.Width.Point / 2, 40), XStringFormats.TopLeft);
gfx.DrawString("$100.00", fontRegular, XBrushes.Black, new XRect(page.Width.Point / 2 + 40, 380, page.Width.Point / 2, 40), XStringFormats.TopLeft);

gfx.DrawString("Item 2", fontRegular, XBrushes.Black, new XRect(40, 400, page.Width.Point / 2, 40), XStringFormats.TopLeft);
gfx.DrawString("$200.00", fontRegular, XBrushes.Black, new XRect(page.Width.Point / 2 + 40, 400, page.Width.Point / 2, 40), XStringFormats.TopLeft);

gfx.DrawString("Total", fontSubTitle, XBrushes.Black, new XRect(40, 420, page.Width.Point / 2, 40), XStringFormats.TopLeft);
gfx.DrawString("$300.00", fontSubTitle, XBrushes.Black, new XRect(page.Width.Point / 2 + 40, 420, page.Width.Point / 2, 40), XStringFormats.TopLeft);

// Save the document
string filename = "Invoice.pdf";
document.Save(filename);

// Open the document
Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });