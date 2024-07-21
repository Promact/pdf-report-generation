const chromium = require("@sparticuz/chromium");
const puppeteer = require("puppeteer-core");
const { S3Client, PutObjectCommand } = require("@aws-sdk/client-s3");

console.log("Loading function");

exports.handler = async (event, context) => {
  let result = null;
  let browser = null;
  try {
    browser = await puppeteer.launch({
      args: chromium.args,
      defaultViewport: chromium.defaultViewport,
      executablePath: await chromium.executablePath(),
      headless: chromium.headless,
    });

    const page = await browser.newPage();
    await page.setContent(event.html);
    const pdfBuffer = await page.pdf({ format: "A4" });

    const s3Client = new S3Client({
      region: process.env.AWS_BUCKET_REGION,
      credentials: {
        accessKeyId: process.env.ACCESS_KEY,
        secretAccessKey: process.env.SECRET_KEY,
      },
    });
    const bucketName = process.env.AWS_BUCKET_NAME;
    const command = new PutObjectCommand({
      Bucket: bucketName,
      Key: `pdfs/${Date.now()}.pdf`,
      Body: pdfBuffer,
    });
    try {
      const response = await s3Client.send(command);
      console.log(response);
    } catch (err) {
      console.error(err);
    }
    result = `PDF saved to ${command.Bucket}/${command.Key}`;
  } catch (error) {
    console.error(error);
    throw new Error("Failed to generate PDF");
  } finally {
    if (browser !== null) {
      await browser.close();
    }
  }
  return result;
};
