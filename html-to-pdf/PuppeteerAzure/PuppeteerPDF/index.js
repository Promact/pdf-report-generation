const { default: puppeteer } = require("puppeteer-core");

// Helper function to get PDF options with defaults
const getPdfOptions = (options) => {
  return {
    format: options && options.format ? options.format : "A4",
    printBackground:
      options && options.printBackground !== undefined
        ? options.printBackground
        : true,
    margin:
      options && options.margin
        ? options.margin
        : {
            top: "1in",
            right: "1in",
            bottom: "1in",
            left: "1in",
          },
    width: options && options.width !== undefined ? options.width : undefined,
    height:
      options && options.height !== undefined ? options.height : undefined,
    landscape:
      options && options.landscape !== undefined ? options.landscape : false,
    displayHeaderFooter:
      options && options.displayHeaderFooter !== undefined
        ? options.displayHeaderFooter
        : false,
    headerTemplate:
      options && options.headerTemplate ? options.headerTemplate : "",
    footerTemplate:
      options && options.footerTemplate ? options.footerTemplate : "",
  };
};

module.exports = async (context, req) => {
  const body = req.body;
  const url = body?.url;
  const html = body?.html;
  const options = body?.options;

  if (!url && !html) {
    return {
      statusCode: 400,
      body: JSON.stringify({ error: "Either 'url' or 'html' is required" }),
    };
  }

  let browser = null;

  try {
    context.log("Launching browser");
    browser = await puppeteer.launch({
      args: [
        "--disable-gpu",
        "--disable-dev-shm-usage",
        "--disable-setuid-sandbox",
        "--no-first-run",
        "--no-sandbox",
        "--no-zygote",
        "--deterministic-fetch",
        "--disable-features=IsolateOrigins",
        "--disable-site-isolation-trials",
      ],
      executablePath: "./chrome-linux/chrome",
      headless: true,
    });
    context.log("Browser launched");

    const page = await browser.newPage();
    context.log("Page created.");

    if (url) {
      await page.goto(url, { waitUntil: "networkidle2" });
      context.log("URL is loaded on page");
    } else {
      await page.setContent(html, { waitUntil: "networkidle2" });
      context.log("Content is set on page");
    }

    context.log("Generating PDF buffer...");
    const pdfBuffer = await page.pdf(getPdfOptions(options));
    context.log("PDF buffer is generated");

    await browser.close();
    context.log("Browser closed.");

    //Return the pdf buffer
    context.log("Returning the response...");
    context.res = {
      status: 200,
      headers: {
        "Content-Type": "application/pdf",
        "Content-Length": pdfBuffer.length.toString(),
        "Content-Disposition": 'attachment; filename="output.pdf"',
      },
      body: pdfBuffer,
    };
  } catch (error) {
    context.log("Error occurred!");
    if (browser !== null) {
      await browser.close();
      context.log("Browser closed.");
    }
    context.res = {
      statusCode: 500,
      body: `Failed to execute web automation task. Error message: ${error.message}`,
    };
  }
};
