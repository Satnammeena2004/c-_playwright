using Microsoft.Playwright;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Utilities;

public static class AuthHelper
{
    public static string StorageFile => "Artifacts/auth-state.json";
    public static async Task SaveLoginState(IBrowser browser)
    {
        Directory.CreateDirectory("Artifacts");

        var context = await browser.NewContextAsync();
        var page    = await context.NewPageAsync();

        await page.GotoAsync(Config.BaseUrl + "/auth/login");
        await page.GetByPlaceholder("Your email").FillAsync(Config.CustomerEmail);
        await page.GetByPlaceholder("Password").FillAsync(Config.CustomerPassword);
        await page.Locator("//input[@value='Login']").ClickAsync();
        await page.WaitForURLAsync(new System.Text.RegularExpressions.Regex(".*/account"));

        await context.StorageStateAsync(new() { Path = StorageFile });
        await context.CloseAsync();
    }
}
