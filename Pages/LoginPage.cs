using Microsoft.Playwright;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Pages;

public class LoginPage : BasePage
{
    private const string LoginPath = "/auth/login";

    public LoginPage(IPage page) : base(page) { }

    // Locators
    public ILocator EmailInput => Page.GetByPlaceholder("Your email");
    public ILocator PasswordInput => Page.GetByPlaceholder("Your password");
    public ILocator LoginButton => Page.Locator("//input[@value='Login']");
    public ILocator ErrorMessage => Page.GetByText("Invalid email or password");
    public ILocator EmailError => Page.GetByText("Email is required");
    public ILocator PasswordError => Page.GetByText("Password is required");

    // Actions
    public async Task GoToAsync()
        => await Page.GotoAsync(Config.BaseUrl + LoginPath);

    public async Task FillEmail(string email)
    {
        await EmailInput.FillAsync(email);

    }

    public async Task FillPassword(string password)
    {

        await PasswordInput.FillAsync(password);
    }

    public async Task ClickLoginButton()
    {
        await LoginButton.ClickAsync();

    }

    public async Task LoginAs(string email, string password)
    {
        await GoToAsync();
        await FillEmail(email);
        await FillPassword(password);
        await ClickLoginButton();
    }
}
