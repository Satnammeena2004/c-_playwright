using NUnit.Framework;
using PlaywrightAssessment.Pages;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Tests;

[TestFixture]
public class CheckoutFlowTests : BaseTest
{
    private HomePage homePage = null!;

    public override BrowserNewContextOptions ContextOptions()
    {
        var storageFile = AuthHelper.StorageFile;
        if (File.Exists(storageFile))
            return new BrowserNewContextOptions { StorageStatePath = storageFile };

        return base.ContextOptions();
    }

    [SetUp]
    public void InitPages()
    {
        homePage = new HomePage(Page);
    }

    [Test]
    [Category("Smoke")]
    [Category("Product")]
    public async Task Homepage_DisplaysProducts()
    {
        await homePage.GoToAsync();
        var count = await homePage.GetProductCount();
        Assert.That(count, Is.GreaterThan(0), "At least one product should be visible on homepage");
    }

    [Test]
    [Category("Regression")]
    [Category("Search")]
    [Category("Product")]

    public async Task Search_Pliers_ReturnsRelevantResults()
    {
        await homePage.GoToAsync();
        await homePage.SearchFor("Pliers");

        var cards = Page.Locator(".card");
        await Expect(cards.First).ToBeVisibleAsync();

        var firstTitle = Page.Locator(".card-title").First;
        await Expect(firstTitle).ToContainTextAsync("Pliers");
    }


    [Test]
    [Category("Regression")]
    [Category("Product")]
    public async Task ProductDetail_ShowsEssentialInfo()
    {
        await homePage.GoToAsync();
        await homePage.ClickFirstProduct();
        ProductPage productPage = new ProductPage(Page);
        await Expect(productPage.ProductTitle).ToBeVisibleAsync();
        await Expect(productPage.ProductPrice).ToBeVisibleAsync();
        await Expect(productPage.AddToCart).ToBeEnabledAsync();
    }

    [Test]
    [Category("Regression")]
    [Category("Product")]

    public async Task AddToCart_UpdatesCartBadge()
    {
        await homePage.GoToAsync();
        await homePage.SearchFor("Hammer");
        await homePage.ClickFirstProduct();
        ProductPage productPage = new ProductPage(Page);
        await productPage.AddFirstProductToCart();

        await Expect(productPage.CartBadge).ToHaveTextAsync("1");
    }

    [Test]
    [Category("Regression")]
    [Category("Search")]
        public async Task SearchNoResults_ShowsEmptyMessage()
    {
        await homePage.GoToAsync();
        await homePage.SearchFor("xyznonexistentproduct123");

        var noResults = Page.GetByText("There are no products found.");
        await Expect(noResults).ToBeVisibleAsync();
    }
}
