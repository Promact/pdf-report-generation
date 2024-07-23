const { jsPDF } = window.jspdf;

function generatePDF() {
  const doc = new jsPDF();
  doc.text("Hello, world!", 10, 10);
  doc.save("example.pdf");
}

function generateShapesAndFontsPDF() {
  const doc = new jsPDF();
  doc.setFont("helvetica", "bold");
  doc.setFontSize(20);
  doc.text("Bold Helvetica", 20, 20);
  doc.setFont("times", "italic");
  doc.setFontSize(16);
  doc.text("Italic Times", 20, 30);
  doc.setDrawColor(0, 255, 0);
  doc.setLineWidth(1);
  doc.rect(20, 40, 50, 20);
  doc.setDrawColor(255, 0, 0);
  doc.circle(50, 80, 10);
  doc.setDrawColor(0, 0, 255);
  doc.triangle(20, 100, 60, 100, 40, 130);
  doc.save("shapes_and_fonts.pdf");
}

function generateTablePDF() {
  const doc = new jsPDF();
  const data = [
    ["ID", "Name", "Age"],
    ["1", "John Doe", "25"],
    ["2", "Jane Smith", "30"],
    ["3", "Sam Johnson", "22"],
  ];
  doc.autoTable({
    head: [data[0]],
    body: data.slice(1),
  });
  doc.save("table.pdf");
}

function generateHyperlinkPDF() {
  const doc = new jsPDF();
  doc.textWithLink("Open jsPDF Documentation", 20, 20, {
    url: "https://rawgit.com/MrRio/jsPDF/master/docs/jsPDF.html",
  });
  doc.save("hyperlink.pdf");
}

function generateImagePDF() {
  const doc = new jsPDF();
  const imgData = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQEAAAAAAAD..."; // Replace with actual base64 encoded image data
  doc.addImage(imgData, "JPEG", 15, 40, 180, 160);
  doc.save("image.pdf");
}

function generateMultiLineTextPDF() {
  const doc = new jsPDF();
  const text = `This is an example of multi-line text in jsPDF.
You can add multiple lines of text easily by providing a string with new line characters (\n) in it.
This makes it convenient to format paragraphs and other text blocks.`;
  doc.text(text, 10, 10);
  doc.text("Centered text", doc.internal.pageSize.getWidth() / 2, 50, {
    align: "center",
  });
  doc.text("Right aligned text", doc.internal.pageSize.getWidth() - 10, 60, {
    align: "right",
  });
  doc.save("multi_line_text.pdf");
}
