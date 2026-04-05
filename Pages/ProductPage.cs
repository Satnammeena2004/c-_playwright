using Microsoft.Playwright;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Pages;

public class ProductPage : BasePage
{
    public ProductPage(IPage page) : base(page) { }
    public ILocator ProductCards => Page.Locator("//a[@class='card']");
    public ILocator AddToCart => Page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" });
    public ILocator CartBadge => Page.Locator("//span[@id='lblCartCount']");
    public ILocator ProductTitle => Page.Locator("h1").First;
    public ILocator ProductPrice => Page.GetByText("$").First;


    public async Task AddFirstProductToCart()
    {
        await AddToCart.ClickAsync();
    }



}