using PuppeteerSharp;
using PuppeteerSharp.Media;

//File path to your HTML script
var html = File.ReadAllText("HTML_FILE_PATH");

//Downloaded chrome instance will be used
await new BrowserFetcher(new BrowserFetcherOptions()
{
    Browser = SupportedBrowser.Chrome
}).DownloadAsync();

//Browser launch options
var browser = await Puppeteer.LaunchAsync(new LaunchOptions
{
    Headless = true
});

using var page = await browser.NewPageAsync();
await page.SetContentAsync(html);
await page.PdfAsync("outputFile.pdf", new PdfOptions
{
    Format = PaperFormat.A4,
    DisplayHeaderFooter = true,
    MarginOptions = new MarginOptions
    {
        Top = "20px",
        Right = "20px",
        Bottom = "40px",
        Left = "20px"
    },
    FooterTemplate = "<div id=\"footer-template\" style=\"font-size:10px !important; color:#808080; padding-left:10px\">Footer Text</div>"
});