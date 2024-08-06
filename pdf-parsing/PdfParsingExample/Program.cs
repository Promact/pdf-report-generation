using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

class Program
{
    static void Main()
    {
        string pdfPath = "<pdf-path>";
        string searchText = "<your search text>";

        ExtractTextFromPdf(pdfPath);
        ExtractTablesFromPdf(pdfPath);
        ExtractMetadataFromPdf(pdfPath);
        SearchTextInPdf(pdfPath, searchText);
    }

    static void ExtractTextFromPdf(string pdfPath)
    {
        using (PdfDocument document = PdfDocument.Open(pdfPath))
        {
            foreach (Page page in document.GetPages())
            {
                string text = page.Text;
                Console.WriteLine($"Text on page {page.Number}:\n{text}");
            }
        }
    }

    static void ExtractTablesFromPdf(string pdfPath)
    {
        using (PdfDocument document = PdfDocument.Open(pdfPath))
        {
            foreach (Page page in document.GetPages())
            {
                var words = page.GetWords().GroupBy(w => w.BoundingBox.Bottom);
                foreach (var row in words)
                {
                    string rowText = string.Join(" | ", row.Select(w => w.Text));
                    Console.WriteLine($"Row on page {page.Number}: {rowText}");
                }
            }
        }
    }

    static void ExtractMetadataFromPdf(string pdfPath)
    {
        using (PdfDocument document = PdfDocument.Open(pdfPath))
        {
            var info = document.Information;
            Console.WriteLine($"Title: {info.Title}");
            Console.WriteLine($"Author: {info.Author}");
            Console.WriteLine($"Creator: {info.Creator}");
            Console.WriteLine($"Producer: {info.Producer}");
            Console.WriteLine($"Creation Date: {info.CreationDate}");
            Console.WriteLine($"Modification Date: {info.ModifiedDate}");
        }
    }

    static void SearchTextInPdf(string pdfPath, string searchText)
    {
        using (PdfDocument document = PdfDocument.Open(pdfPath))
        {
            foreach (Page page in document.GetPages())
            {
                foreach (var word in page.GetWords())
                {
                    if (word.Text.Contains(searchText))
                    {
                        Console.WriteLine($"Found '{searchText}' on page {page.Number}");
                    }
                }
            }
        }
    }
}
