using Microsoft.Playwright;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    public ILocator SearchInput => Page.GetByPlaceholder("Search");
    public ILocator SearchButton => Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
    public ILocator ProductCards => Page.Locator("//a[@class='card']");


 

    // Actions
    public async Task GoToAsync()
    {

        await Page.GotoAsync(Config.BaseUrl);
        await WaitForPageLoad();
    }

    public async Task SearchFor(string term)
    {
        await SearchInput.FillAsync(term);
        await SearchButton.ClickAsync();
        await WaitForPageLoad();
    }

    public async Task ClickFirstProduct()
    {
        await ProductCards.First.ClickAsync();
    }


    // public async Task AddFirstProductToCart()
    // {
    //     ProductPage productPage = new ProductPage(Page);
    //     await productPage.AddFirstProductToCart();
    // }





    public async Task<int> GetProductCount()
    {
        Console.WriteLine(ProductCards);
        return await ProductCards.CountAsync();

    }

}
