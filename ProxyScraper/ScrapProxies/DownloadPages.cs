using ProxyScrapper.Tools;

namespace ProxyScraper.ScrapProxies;
internal static class DownloadPages
{
    public static async Task<List<string>> Download()
    {
        List<Task<string>> tasks = new List<Task<string>>();
        GetProxySources.GetSources().ForEach(source =>
        {
            tasks.Add(HttpHelper.DownloadPage(source));
        });
        var results = await Task.WhenAll(tasks.ToArray());
        return results.ToList().Where(x => x != string.Empty).ToList();
    }
}
