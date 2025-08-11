using Microsoft.Playwright;
using System.Diagnostics;
using System.Reflection;
using System.Text;

// Create an instance of Playwright
using var playwright = await Playwright.CreateAsync();

// Launch a Chromium browser instance
await using var browser = await playwright.Chromium.LaunchAsync();

// Create a temporary directory to store the generated files
var tempDir = Directory.CreateTempSubdirectory("pdfs_");

// Define the path for the HTML file that will be used as input for the PDF
var outputPath = Path.Combine(tempDir.FullName, "pdf-input.html");

// Load the embedded HTML template resource from the assembly
using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CreatePdfs.Playwright.html-template.html");
if (stream == null)
{
    throw new InvalidOperationException("The HTML template resource could not be found.");
}

// Read the HTML template into a byte buffer
Span<byte> buffer = new byte[stream.Length];
stream.ReadExactly(buffer);

// Convert the byte buffer to a UTF-8 string
var templateHtml = Encoding.UTF8.GetString(buffer);

// Generate placeholder content for the body using Lorem.NETCore
// Settings: Generate 20 paragraphs with between 3 and 8 sentences per paragraph, and between 8 and 10 words per sentence.
var generatedBody = string.Join("</p><p>", LoremNETCore.Generate.Paragraphs(8, 20, 3, 8, 20));

// Replace placeholders in the HTML template with actual content
using var outputFile = File.CreateText(outputPath);
outputFile.Write(templateHtml!
    .Replace("{{title}}", "Hello, World!") // Replace the title placeholder
    .Replace("{{body}}", generatedBody) // Replace the body placeholder with generated content
);
outputFile.Close();

// Create a new browser page
var page = await browser.NewPageAsync();

// Navigate to the generated HTML file
await page.GotoAsync(outputPath);

// Generate a PDF from the HTML content
await page.PdfAsync(new PagePdfOptions()
{
    DisplayHeaderFooter = true, // Enable header and footer in the PDF
    Landscape = false, // Use portrait orientation
    PreferCSSPageSize = true, // Use CSS-defined page size
    Tagged = true, // Enable tagged PDF for accessibility
    Path = outputPath.Replace(".html", ".pdf"), // Define the output path for the PDF
    Outline = true, // Include an outline (bookmarks) in the PDF
});

// Open the generated PDF file using the default PDF viewer
var pdfPath = outputPath.Replace(".html", ".pdf");
Process.Start(new ProcessStartInfo
{
    FileName = pdfPath,
    UseShellExecute = true // Use the default application associated with the file type
});
