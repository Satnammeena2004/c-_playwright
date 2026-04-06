using NUnit.Framework;
using PlaywrightAssessment._Config;

namespace PlaywrightAssessment.Tests;

[TestFixture]
public class NetworkMockTests : BaseTest
{
    [Test]
    public async Task MockProductApi_ShowsMockedData()
    {
        await Page.RouteAsync("**/products**", async route =>
        {
            await route.FulfillAsync(new()
            {
                Status = 200,
                ContentType = "application/json",
                Body = """
                {
                  "data": [
                    { "id": 99, "name": "Mock Hammer Pro", "price": 49.99 }
                  ],
                  "total": 1
                }
                """
            });
        });

        await Page.GotoAsync(Config.BaseUrl + "/products");
        await Expect(Page.GetByText("Mock Hammer Pro")).ToBeVisibleAsync();
    }


}
