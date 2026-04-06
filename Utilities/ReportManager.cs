// Utilities/ReportManager.cs
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace PlaywrightAssessment.Utilities;

public static class ReportManager
{
    // The main report object — shared across all tests
    private static ExtentReports? _extent;

    public static ExtentReports Extent
    {
        get
        {
            // Create it only once, reuse after that
            if (_extent == null)
            {
                Directory.CreateDirectory("Artifacts/Report");

                // This is the HTML file that gets created
                var htmlReporter = new ExtentHtmlReporter("Artifacts/Report/index.html");

                htmlReporter.Config.DocumentTitle = "Playwright C# Test Report";
                htmlReporter.Config.ReportName = "Automation Test Results";

                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
            }

            return _extent;
        }
    }

    // Call this at the very end to write the HTML file to disk
    public static void SaveReport()
    {
        _extent?.Flush();
    }
}