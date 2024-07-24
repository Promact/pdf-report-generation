using IronPdf;
using IronPdf.Rendering;
using IronPdf.Engines.Chrome;

//Apply license
License.LicenseKey = "<License-Key>";

// Instantiate Renderer
var renderer = new ChromePdfRenderer()
{
    RenderingOptions = new ChromePdfRenderOptions()
    {
        UseMarginsOnHeaderAndFooter = UseMargins.None,
        CreatePdfFormsFromHtml = false,
        CssMediaType = PdfCssMediaType.Print,
        CustomCssUrl = null,
        EnableJavaScript = false,
        Javascript = null,
        JavascriptMessageListener = null,
        FirstPageNumber = 0,
        GrayScale = false,
        HtmlHeader = null,
        HtmlFooter = null,
        InputEncoding = null,
        MarginBottom = 0,
        MarginLeft = 0,
        MarginRight = 0,
        MarginTop = 0,
        PaperOrientation = PdfPaperOrientation.Portrait,
        PaperSize = PdfPaperSize.Letter,
        PrintHtmlBackgrounds = false,
        TextFooter = null,
        TextHeader = null,
        Timeout = 0,
        Title = null,
        ForcePaperSize = false,
        ViewPortHeight = 0,
        ViewPortWidth = 0,
        Zoom = 0,
        FitToPaperMode = FitToPaperModes.Zoom
    },
    LoginCredentials = null
};

// Create a PDF from a HTML string using C#
var pdfFromHtmlString = renderer.RenderHtmlAsPdf("<HTML-String>");

// Create a PDF from a HTML file using C#
var pdfFromHtmlFile = renderer.RenderHtmlFileAsPdf("<HTML-File-Path>");

// Export to a file or Stream
pdfFromHtmlString.SaveAs("<filename>.pdf");
pdfFromHtmlFile.SaveAs("<filename>.pdf");