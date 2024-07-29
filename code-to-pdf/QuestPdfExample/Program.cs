using QuestPDF.Infrastructure;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

// Set the license type for QuestPDF.
// Ensure you are eligible to use the Community license.
// For details, visit: https://www.questpdf.com/pricing.html
Settings.License = LicenseType.Community;

// Create a new PDF document
Document.Create(container =>
{
    container.Page(page =>
    {
        // Set page margins and size
        page.Margin(1.3f, Unit.Centimetre);
        page.Size(PageSizes.A4);

        // Define the header of the page
        page.Header()
            .PaddingBottom(20)
            .Text("Invoice")           // Header text
            .WordSpacing(1)
            .FontSize(20)              // Font size for header
            .AlignCenter();            // Center align header text
  

        // Define the content of the page
        page.Content()
            .Column(column =>
            {
                column.Spacing(5);  // Space between items in the column

                // Add company information
                column.Item().Text("Tech Solutions Inc.")
                    .FontSize(14)      // Font size for company name
                    .AlignCenter();   // Center align company name
                column.Item().Text("456 Technology Blvd.")
                    .FontSize(12)      // Font size for address
                    .AlignCenter();   // Center align address
                column.Item().Text("San Francisco, CA, 94107")
                    .FontSize(12)      // Font size for address
                    .AlignCenter();   // Center align address

                column.Item().Text("");  // Empty line for spacing

                // Add invoice details
                column.Item().Text($"Invoice Number: 001")
                    .FontSize(12);     // Font size for invoice number
                column.Item().Text($"Date: {DateTime.Now:yyyy-MM-dd}")
                    .FontSize(12);     // Font size for date

                column.Item().Text("");  // Empty line for spacing

                // Define the table for invoice items
                column.Item().Table(table =>
                {
                    // Define table columns
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(1); // Column for Quantity
                        columns.RelativeColumn(5); // Column for Description
                        columns.RelativeColumn(2); // Column for Unit Price
                        columns.RelativeColumn(4); // Column for Total
                    });

                    // Define table header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Qty");          // Quantity header
                        header.Cell().Element(CellStyle).Text("Description");   // Description header
                        header.Cell().Element(CellStyle).Text("Unit Price");    // Unit Price header
                        header.Cell().Element(CellStyle).Text("Total");         // Total header

                        // Define cell style for header
                        static IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Border(0.5f)               // Border thickness for header cells
                                .BorderColor(Colors.Black)     // Border color for header cells
                                .PaddingHorizontal(3)
                                .PaddingVertical(3)            // Vertical padding for header cells
                                .DefaultTextStyle(x => x.FontSize(12));  // Font size for header text
                        }
                    });

                    // Define table rows with item data
                    string[,] items = {
                        { "1", "Wireless Mouse", "$25.00", "$25.00" },
                        { "2", "Mechanical Keyboard", "$75.00", "$150.00" },
                        { "3", "USB-C Hub", "$40.00", "$120.00" }
                    };

                    // Loop through items and add rows to the table
                    for (int i = 0; i < items.GetLength(0); i++)
                    {
                        table.Cell().Element(CellStyle).Text(items[i, 0]);  // Quantity
                        table.Cell().Element(CellStyle).Text(items[i, 1]);  // Description
                        table.Cell().Element(CellStyle).Text(items[i, 2]);  // Unit Price
                        table.Cell().Element(CellStyle).Text(items[i, 3]);  // Total

                        // Define cell style for rows
                        static IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Border(0.5f)               // Border thickness for row cells
                                .BorderColor(Colors.Black)  // Border color for row cells
                                .PaddingHorizontal(3)
                                .PaddingVertical(3)            // Vertical padding for row cells
                                .DefaultTextStyle(x => x.FontSize(12));  // Font size for row text
                        }
                    }
                });

                column.Item().Text("");  // Empty line for spacing

                // Add total amount
                column.Item().AlignRight().Text("Total Amount: $295.00")
                    .FontSize(12);  // Font size for total amount
            });

        // Define the footer of the page
        page.Footer()
            .AlignCenter()
            .Text(text =>
            {
                text.Span("Page ");        // Page text
                text.CurrentPageNumber();  // Current page number
            });
    });
})
// Generate and save the PDF to a file named "invoice.pdf"
.GeneratePdf("invoice.pdf");
