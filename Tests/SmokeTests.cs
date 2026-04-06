using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Tests;


[TestFixture]
public class SmokeTests : BaseTest
{
    [Test]
    public async Task HomepageTitleIsCorrect()
    {
        await Page.GotoAsync(Config.BaseUrl);
        await Expect(Page).ToHaveTitleAsync(new System.Text.RegularExpressions.Regex(".*Practice Software Testing.*"));
    }

    [Test]
    public async Task HomepageLoads_ProductsAreVisible()
    {
        await Page.GotoAsync(Config.BaseUrl);
        var cards = Page.Locator(".card");
        await Expect(cards.First).ToBeVisibleAsync();
    }

    [Test]
    public async Task NavigationMenu_IsVisible()
    {
        await Page.GotoAsync(Config.BaseUrl);
        var nav = Page.Locator("nav");
        await Expect(nav).ToBeVisibleAsync();
    }

    [Test]
    public async Task Homepage_ScreenshotCaptured()
    {
        await Page.GotoAsync(Config.BaseUrl);
        await Page.ScreenshotAsync(new() { Path = "Artifacts/homepage.png", FullPage = true });
        Assert.Pass("Homepage screenshot captured successfully.");
    }
}
