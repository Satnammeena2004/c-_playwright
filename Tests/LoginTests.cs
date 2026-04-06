using NUnit.Framework;
using PlaywrightAssessment.Pages;

namespace PlaywrightAssessment.Tests;

[TestFixture]

public class LoginTests : BaseTest
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public void InitPages()
    {
        _loginPage = new LoginPage(Page);
    }


    [Test]
    [Category("Smoke")]
    [Category("Login")]
    public async Task ValidLogin_RedirectsToAccountPage()
    {
        await _loginPage.LoginAs(Config.CustomerEmail, Config.CustomerPassword);
        await Expect(Page).ToHaveURLAsync(new Regex(".*/account"));
    }

    [Test]
     [Category("Smoke")]
    [Category("Login")]
    public async Task ValidLogin_ThenLogout_RedirectsToLogin()
    {
        await _loginPage.LoginAs(Config.CustomerEmail, Config.CustomerPassword);
        await Expect(Page).ToHaveURLAsync(new Regex(".*/account"));
        await Page.Locator("//a[@id='menu']").ClickAsync();
        await Page.GetByText("Sign out").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex(".*/auth/login"));
    }


    [Test]
     [Category("Regression")]
    [Category("Login")]
    public async Task InvalidPassword_ShowsErrorMessage()
    {
        await _loginPage.LoginAs(Config.CustomerEmail, "wrongpassword999");

        await Expect(_loginPage.ErrorMessage).ToBeVisibleAsync();
    }


    [Test]
     [Category("Regression")]
    [Category("Login")]
    public async Task EmptyFields_ShowsValidationErrors()
    {
        await _loginPage.GoToAsync();
        await _loginPage.ClickLoginButton();

        await Expect(_loginPage.EmailError).ToBeVisibleAsync();
    }

    private readonly static object[] data =
        [
            new object[]{"wrong@email.com", "wrongpass", false},
        new object[]{"wrong2@email.com", "wrongpass2", false},
        new object[]{Config.CustomerEmail, Config.CustomerPassword, true},
    ];
    [Test]
    [TestCaseSource(nameof(data))]
    public async Task LoginTestWithData(string email, string password, bool expected)
    {
        await _loginPage.LoginAs(email, password);
        if (expected)
        {
            await Expect(Page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex(".*/account"));

        }
        else
        {
            await Expect(Page.GetByText("Invalid email or password")).ToBeVisibleAsync();
        }
    }
}
