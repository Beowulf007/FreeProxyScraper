namespace ProxyScrapper.Tools;
internal static class HttpHelper
{
    private static HttpClient _client;

    static HttpHelper()
    {
        _client = new HttpClient();
        _client.Timeout = TimeSpan.FromSeconds(30);
    }
    public static async Task<string> DownloadPage(string url)
    {
        try
        {
            var response = await _client.GetAsync(url);
            await Console.Out.WriteLineAsync($"{url} indirildi.");
            return await response.Content.ReadAsStringAsync();
        }
        catch(Exception ex) 
        {
            Logger.Log($"{url} - {ex.Message}");
            return await Task.FromResult(String.Empty);
        }
    }
}
