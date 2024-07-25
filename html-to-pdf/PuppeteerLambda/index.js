const chromium = require("@sparticuz/chromium");
const puppeteer = require("puppeteer-core");

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

exports.handler = async (event) => {
  const body = event.body;
  const { url, html, options } = JSON.parse(body);

  if (!url && !html) {
    return {
      statusCode: 400,
      body: JSON.stringify({ error: "Either 'url' or 'html' is required" }),
    };
  }

  let browser = null;

  try {
    browser = await puppeteer.launch({
      args: chromium.args,
      defaultViewport: chromium.defaultViewport,
      executablePath: await chromium.executablePath(),
      headless: chromium.headless,
    });

    const page = await browser.newPage();

    if (url) {
      await page.goto(url, { waitUntil: "networkidle2" });
    } else {
      await page.setContent(html, { waitUntil: "networkidle2" });
    }

    const pdfBuffer = await page.pdf(getPdfOptions(options));
    await browser.close();

    return {
      statusCode: 200,
      headers: {
        "Content-Type": "application/pdf",
        "Content-Disposition": "attachment; filename=output.pdf",
      },
      body: pdfBuffer.toString("base64"),
      isBase64Encoded: true,
    };
  } catch (error) {
    if (browser !== null) {
      await browser.close();
    }
    console.error("Error:", error);
    return {
      statusCode: 500,
      body: JSON.stringify({ error: "Internal Server Error" }),
    };
  }
};
