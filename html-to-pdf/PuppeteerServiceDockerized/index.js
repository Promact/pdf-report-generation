const express = require("express");
const puppeteer = require("puppeteer");
const app = express();
const port = 3000;

app.use(express.json());

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

// Helper function to get Screenshot options with defaults
const getScreenshotOptions = (options) => {
  return {
    fullpage: options && options.fullpage ? options.fullpage : true,
  };
};

// Endpoint to generate PDF or capture screenshot
app.post("/pdf", async (req, res) => {
  const { url, html, options } = req.body;

  if (!url && !html) {
    return res.status(400).send("Either 'url' or 'html' is required");
  }

  try {
    const browser = await puppeteer.launch({
      args: ["--no-sandbox", "--disable-setuid-sandbox"],
    });
    const page = await browser.newPage();

    if (url) {
      await page.goto(url, { waitUntil: "networkidle2" });
    } else {
      await page.setContent(html, { waitUntil: "networkidle2" });
    }

    let resultBuffer;
    resultBuffer = await page.pdf(getPdfOptions(options));

    await browser.close();

    res.set({
      "Content-Type": "application/pdf",
      "Content-Disposition": "attachment; filename=output.pdf",
      "Content-Length": resultBuffer.length,
    });

    res.send(resultBuffer);
  } catch (error) {
    console.error("Error generating PDF:", error);
    res.status(500).send("Error generating PDF");
  }
});

// Endpoint to capture screenshot
app.post("/screenshot", async (req, res) => {
  const { url, options } = req.body;

  if (!url) {
    return res.status(400).send("URL is required");
  }

  try {
    const browser = await puppeteer.launch({
      args: ["--no-sandbox", "--disable-setuid-sandbox"],
    });
    const page = await browser.newPage();

    await page.goto(url, { waitUntil: "networkidle2" });

    const screenshotBuffer = await page.screenshot(
      getScreenshotOptions(options)
    );

    await browser.close();

    res.set({
      "Content-Type": "image/png",
      "Content-Disposition": "attachment; filename=screenshot.png",
      "Content-Length": screenshotBuffer.length,
    });

    res.send(screenshotBuffer);
  } catch (error) {
    console.error("Error capturing screenshot:", error);
    res.status(500).send("Error capturing screenshot");
  }
});

app.listen(port, () => {
  console.log(`Server is running at http://localhost:${port}`);
});
