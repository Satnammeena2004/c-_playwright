namespace PlaywrightAssessment._Config;

public static class Config
{
    public static string BaseUrl =>
        Environment.GetEnvironmentVariable("BASE_URL")
        ?? "https://practicesoftwaretesting.com";

    public static string CustomerEmail =>
        Environment.GetEnvironmentVariable("CUSTOMER_EMAIL")
        ?? "customer@practicesoftwaretesting.com";

    public static string CustomerPassword =>
        Environment.GetEnvironmentVariable("CUSTOMER_PASSWORD")
        ?? "welcome01";

    public static string ApiBaseUrl =>
        Environment.GetEnvironmentVariable("API_BASE_URL")
        ?? "https://dummyjson.com";
}
