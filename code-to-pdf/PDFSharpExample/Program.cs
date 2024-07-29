using PdfSharp.Drawing;
using PdfSharp.Pdf;

string dest = "invoice.pdf";

// Create a new PDF document
PdfDocument document = new PdfDocument();
document.Info.Title = "Invoice";

// Create an empty page
PdfPage page = document.AddPage();

// Get an XGraphics object for drawing
XGraphics gfx = XGraphics.FromPdfPage(page);

// Create fonts
XFont titleFont = new XFont("Verdana", 20, XFontStyleEx.Regular);
XFont headerFont = new XFont("Verdana", 14, XFontStyleEx.Regular);
XFont normalFont = new XFont("Verdana", 12, XFontStyleEx.Regular);

// Add a title
gfx.DrawString("Invoice", titleFont, XBrushes.Black,
    new XRect(0, 40, page.Width.Point, page.Height.Point),
    XStringFormats.TopCenter);

// Add company information
gfx.DrawString("Tech Solutions Inc.", headerFont, XBrushes.Black,
    new XRect(0, 100, page.Width.Point, page.Height.Point),
    XStringFormats.TopCenter);
gfx.DrawString("456 Technology Blvd.", normalFont, XBrushes.Black,
    new XRect(0, 120, page.Width.Point, page.Height.Point),
    XStringFormats.TopCenter);
gfx.DrawString("San Francisco, CA, 94107", normalFont, XBrushes.Black,
    new XRect(0, 140, page.Width.Point, page.Height.Point),
    XStringFormats.TopCenter);

// Add invoice details
gfx.DrawString("Invoice Number: 001", normalFont, XBrushes.Black,
    new XRect(40, 180, page.Width.Point, page.Height.Point),
    XStringFormats.TopLeft);
gfx.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd"), normalFont, XBrushes.Black,
    new XRect(40, 200, page.Width.Point, page.Height.Point),
    XStringFormats.TopLeft);

// Add a table with invoice items
int tableStartY = 240;
int cellHeight = 20;
int columnQtyX = 40;
int columnDescriptionX = 85;
int columnUnitPriceX = 285;
int columnTotalX = 365;

// Header
gfx.DrawRectangle(XPens.Black, columnQtyX, tableStartY, page.Width.Point - 80, cellHeight);
gfx.DrawString("Qty", normalFont, XBrushes.Black, new XRect(columnQtyX + 5, tableStartY + 5, 40, cellHeight), XStringFormats.TopLeft);
gfx.DrawString("Description", normalFont, XBrushes.Black, new XRect(columnDescriptionX + 5, tableStartY + 5, 200, cellHeight), XStringFormats.TopLeft);
gfx.DrawString("Unit Price", normalFont, XBrushes.Black, new XRect(columnUnitPriceX + 5, tableStartY + 5, 80, cellHeight), XStringFormats.TopLeft);
gfx.DrawString("Total", normalFont, XBrushes.Black, new XRect(columnTotalX + 5, tableStartY + 5, 80, cellHeight), XStringFormats.TopLeft);

// Items
string[,] items = {
            { "1", "Wireless Mouse", "$25.00", "$25.00" },
            { "2", "Mechanical Keyboard", "$75.00", "$150.00" },
            { "3", "USB-C Hub", "$40.00", "$120.00" }
        };

// Draw vertical lines for the table
gfx.DrawLine(XPens.Black, columnQtyX, tableStartY, columnQtyX, tableStartY + (items.GetLength(0) + 1) * cellHeight);
gfx.DrawLine(XPens.Black, columnDescriptionX, tableStartY, columnDescriptionX, tableStartY + (items.GetLength(0) + 1) * cellHeight);
gfx.DrawLine(XPens.Black, columnUnitPriceX, tableStartY, columnUnitPriceX, tableStartY + (items.GetLength(0) + 1) * cellHeight);
gfx.DrawLine(XPens.Black, columnTotalX, tableStartY, columnTotalX, tableStartY + (items.GetLength(0) + 1) * cellHeight);
gfx.DrawLine(XPens.Black, page.Width.Point - 40, tableStartY, page.Width.Point - 40, tableStartY + (items.GetLength(0) + 1) * cellHeight);



for (int i = 0; i < items.GetLength(0); i++)
{
    gfx.DrawRectangle(XPens.Black, columnQtyX, tableStartY + (i + 1) * cellHeight, page.Width.Point - 80, cellHeight);
    gfx.DrawString(items[i, 0], normalFont, XBrushes.Black, new XRect(columnQtyX + 5, tableStartY + (i + 1) * cellHeight + 5, 40, cellHeight), XStringFormats.TopLeft);
    gfx.DrawString(items[i, 1], normalFont, XBrushes.Black, new XRect(columnDescriptionX + 5, tableStartY + (i + 1) * cellHeight + 5, 200, cellHeight), XStringFormats.TopLeft);
    gfx.DrawString(items[i, 2], normalFont, XBrushes.Black, new XRect(columnUnitPriceX + 5, tableStartY + (i + 1) * cellHeight + 5, 80, cellHeight), XStringFormats.TopLeft);
    gfx.DrawString(items[i, 3], normalFont, XBrushes.Black, new XRect(columnTotalX + 5, tableStartY + (i + 1) * cellHeight + 5, 80, cellHeight), XStringFormats.TopLeft);
}

// Add total amount
gfx.DrawString("Total Amount: $295.00", normalFont, XBrushes.Black,
    new XRect(0, tableStartY + (items.GetLength(0) + 2) * cellHeight, page.Width.Point - 40, cellHeight),
    XStringFormats.TopRight);

// Save the document
document.Save(dest);