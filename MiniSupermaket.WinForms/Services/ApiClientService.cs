using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms.Services;

public static class AppConfig
{
    /// <summary>true = chạy với dữ liệu giả lập trong bộ nhớ (không cần Web API). false = gọi API thật.</summary>
    public const bool UseMockApi = true;

    /// <summary>Địa chỉ Web API thật (Buổi 2-3). Phải kết thúc bằng dấu '/'.</summary>
    public const string ApiBaseUrl = "https://localhost:7001/api/";
}

public static class ApiClientService
{
    public static HttpClient Client { get; } = CreateClient();

    private static HttpClient CreateClient()
    {
        HttpMessageHandler handler = AppConfig.UseMockApi ? new MockApiHandler() : new HttpClientHandler();
        return new HttpClient(handler)
        {
            BaseAddress = new Uri(AppConfig.ApiBaseUrl),
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    public static void SetToken(string token) =>
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    public static void ClearToken() =>
        Client.DefaultRequestHeaders.Authorization = null;
}
