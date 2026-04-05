using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace PlaywrightAssessment.Tests;

[TestFixture]
public class BaseTest : PageTest
{

    [SetUp]
    public async Task BeforeEachTest()
    {
        Directory.CreateDirectory("Artifacts");

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
            await SaveFailureEvidence();
        else
            await DiscardTrace();
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

        TestContext.AddTestAttachment(screenshotPath, "Failure Screenshot");
        TestContext.AddTestAttachment(tracePath, "Playwright Trace");
    }

    private async Task DiscardTrace()
    {
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