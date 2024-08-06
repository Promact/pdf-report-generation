using System.Diagnostics;

var inputHtml = "<HTML_FILE_PATH>";
var outputPdf = "<OUTPUT_FILE_PATH>";

if (!File.Exists(inputHtml))
{
    Console.WriteLine($"{inputHtml} file not found!");
    return;
}

var processStartInfo = new ProcessStartInfo
{
    // Pass the path of the wkhtmltopdf executable.
    FileName = "<PATH_TO_WKHTMLTOPDF_EXECUTABLE>",
    Arguments = $"{inputHtml} {outputPdf}",
    UseShellExecute = false,
    RedirectStandardOutput = true,
    WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
};

Console.WriteLine($"Starting process with FileName: {processStartInfo.FileName} and Arguments: {processStartInfo.Arguments}");

using (var process = Process.Start(processStartInfo))
{
    process?.WaitForExit();
    if (process?.ExitCode == 0)
    {
        Console.WriteLine("HTML to PDF conversion successful!");
    }
    else
    {
        Console.WriteLine("HTML to PDF conversion failed!");
        Console.WriteLine(process?.StandardOutput.ReadToEnd());
    }
}