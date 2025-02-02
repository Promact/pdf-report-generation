﻿using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

string dest = "invoice.pdf";

// Create a PdfWriter instance
using (PdfWriter writer = new PdfWriter(dest))
{
    // Create a PdfDocument instance
    using (PdfDocument pdf = new PdfDocument(writer))
    {
        // Create a Document instance
        Document document = new Document(pdf);

        // Add a title
        document.Add(new Paragraph("Invoice")
            .SetPaddingBottom(15)
            .SetFontSize(20)
            .SetTextAlignment(TextAlignment.CENTER));

        // Add company information
        document.Add(new Paragraph("Tech Solutions Inc.")
            .SetFontSize(14)
            .SetTextAlignment(TextAlignment.CENTER));
        document.Add(new Paragraph("456 Technology Blvd.")
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.CENTER));
        document.Add(new Paragraph("San Francisco, CA, 94107")
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.CENTER));


        // Add a blank line
        document.Add(new Paragraph(" "));

        // Add invoice details
        document.Add(new Paragraph("Invoice Number: 001")
            .SetFontSize(12));
        document.Add(new Paragraph("Date: " + DateTime.Now.ToString("yyyy-MM-dd"))
            .SetFontSize(12));

        // Add a blank line
        document.Add(new Paragraph(" "));

        // Add a table with invoice items
        Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1, 5, 2, 4 }))
            .UseAllAvailableWidth();
        table.AddHeaderCell("Qty");
        table.AddHeaderCell("Description");
        table.AddHeaderCell("Unit Price");
        table.AddHeaderCell("Total");

        // Sample invoice items
        table.AddCell("1");
        table.AddCell("Wireless Mouse");
        table.AddCell("$25.00");
        table.AddCell("$25.00");

        table.AddCell("2");
        table.AddCell("Mechanical Keyboard");
        table.AddCell("$75.00");
        table.AddCell("$150.00");

        table.AddCell("3");
        table.AddCell("USB-C Hub");
        table.AddCell("$40.00");
        table.AddCell("$120.00");

        // Add table to the document
        document.Add(table);

        // Add a blank line
        document.Add(new Paragraph(" "));

        // Add total amount
        document.Add(new Paragraph("Total Amount: $295.00")
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.RIGHT));

        // Close the document
        document.Close();
    }
}

Console.WriteLine("Invoice PDF created successfully!");
