const puppeteer = require("puppeteer");

async function run() {
  const browser = await puppeteer.launch();

  const page = await browser.newPage();
  await page.goto("https://www.google.com");

  await browser.close();
}

//Error handling
run().catch((error) => {
  console.error(error);
  process.exit(1);
});
