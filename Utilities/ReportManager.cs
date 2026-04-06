// Utilities/ReportManager.cs
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace PlaywrightAssessment.Utilities;

public static class ReportManager
{
    private static ExtentReports? _extent;

    public static ExtentReports Extent
    {
        get
        {
            if (_extent == null)
            {

                var htmlReporter = new ExtentHtmlReporter("Artifacts/Report/index.html");

                htmlReporter.Config.DocumentTitle = "Playwright C# Test Report";
                htmlReporter.Config.ReportName = "Automation Test Results";

                _extent = new ExtentReports();
                _extent.AttachReporter(htmlReporter);
            }

            return _extent;
        }
    }

    public static void SaveReport()
    {
        _extent?.Flush();
    }
}