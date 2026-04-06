using AventStack.ExtentReports;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace PlaywrightAssessment.Tests;

[TestFixture]
public class BaseTest : PageTest
{
    private ExtentTest reportTest = null!;

    [SetUp]
    public async Task BeforeEachTest()
    {
        Directory.CreateDirectory("Artifacts");
        reportTest = ReportManager.Extent.CreateTest(TestContext.CurrentContext.Test.Name);
        await Context.Tracing.StartAsync(new()
        {
            Title = CurrentTestName(),
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }


    [TearDown]
    public async Task AfterEachTest()
    {
        bool testFailed = TestContext.CurrentContext.Result.Outcome.Status
                          == TestStatus.Failed;

        if (testFailed)
        {

            await SaveFailureEvidence();
        }
        else
        {

            await DiscardTrace();
        }
        
        ReportManager.SaveReport();

    }

    private async Task SaveFailureEvidence()
    {
        string testName = CurrentTestName();
        string screenshotPath = $"Artifacts/FAIL-{testName}.png";
        string tracePath = $"Artifacts/trace-{testName}.zip";

        await Page.ScreenshotAsync(new()
        {
            Path = screenshotPath,
            FullPage = true
        });

        await Context.Tracing.StopAsync(new() { Path = tracePath });
        var error = TestContext.CurrentContext.Result.Message ?? "Unknown error";
        reportTest.Fail($"Test failed: {error}");
        reportTest.AddScreenCaptureFromPath(screenshotPath, "Failure Screenshot");

        TestContext.AddTestAttachment(screenshotPath, "Failure Screenshot");
        TestContext.AddTestAttachment(tracePath, "Playwright Trace");
    }

    private async Task DiscardTrace()
    {
        reportTest.Pass("Test passed");
        await Context.Tracing.StopAsync(new());
    }

    private static string CurrentTestName()
    {

        return TestContext.CurrentContext.Test.Name
            .Replace(" ", "_")
            .Replace("(", "")
            .Replace(")", "");
    }
}