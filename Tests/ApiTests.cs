using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.Json;

namespace PlaywrightAssessment.Tests;

[TestFixture]
public class ApiTests : PlaywrightTest
{
    private IAPIRequestContext _api = null!;

    [SetUp]
    public async Task SetupApiContext()
    {
        _api = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = Config.ApiBaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["Content-Type"] = "application/json",
                ["Accept"] = "application/json"
            }
        });
    }


    [Test]
    public async Task GET_Products_Returns200()
    {
        var response = await _api.GetAsync("/products");

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(response.Ok, Is.True);
    }

    [Test]
    public async Task GET_Products_ReturnsNonEmptyList()
    {
        var response = await _api.GetAsync("/products");
        var body = await response.JsonAsync();

        Assert.That(body, Is.Not.Null);

        var products = body!.Value.GetProperty("products");
        Assert.That(products.GetArrayLength(), Is.GreaterThan(0));
    }


    [Test]
    public async Task POST_Login_Valid_ReturnsToken()
    {
        var response = await _api.PostAsync("/auth/login", new APIRequestContextOptions
        {
            DataObject = new
            {
                username = "emilys",
                password = "emilyspass"
            }
        });

        Assert.That(response.Status, Is.EqualTo(200));

        var json = await response.JsonAsync();
        var token = json!.Value.GetProperty("accessToken").GetString();

        Assert.That(token, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    public async Task POST_Login_Invalid_Returns400()
    {
        var response = await _api.PostAsync("/auth/login", new APIRequestContextOptions
        {
            DataObject = new
            {
                username = "wronguser",
                password = "wrongpass"
            }
        });

        Assert.That(response.Status, Is.EqualTo(400));
    }

    [Test]
    public async Task POST_Login_EmptyBody_Returns400()
    {
        var response = await _api.PostAsync("/auth/login", new APIRequestContextOptions
        {
            DataObject = new { }
        });

        Assert.That(response.Status, Is.EqualTo(400));
    }


    [TearDown]
    public async Task TeardownApi()
    {
        await _api.DisposeAsync();
    }
}