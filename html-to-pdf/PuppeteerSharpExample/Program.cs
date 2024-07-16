using PuppeteerSharp;
using PuppeteerSharp.Media;

//File path to your HTML script
var html = File.ReadAllText("HTML_FILE_PATH");

//Set pdf options
var pdfOptions = new PdfOptions()
{
    Format = PaperFormat.A4
};

//Launch chromium instance
using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions
{
    //Set false for debugging
    Headless = true,

    //File path to your chrome exe
    ExecutablePath = "CHROME_EXECUTABLE_PATH"
}))
{
    using (var page = await browser.NewPageAsync())
    {
        await page.SetContentAsync(html);
        await page.PdfAsync("invoice.pdf", pdfOptions);
    }
}