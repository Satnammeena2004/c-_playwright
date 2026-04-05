using Microsoft.Playwright;

namespace PlaywrightAssessment.Pages;

public class BasePage
{
    protected readonly IPage Page;

    public BasePage(IPage page)
    {
        Page = page;
    }

    protected async Task WaitForPageLoad()
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    protected async Task TakeScreenshot(string name)
    {
        Directory.CreateDirectory("Artifacts");
        await Page.ScreenshotAsync(new() { Path = $"Artifacts/{name}.png", FullPage = true });
    }
}
