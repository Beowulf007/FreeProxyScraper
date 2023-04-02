using System.Text.RegularExpressions;

namespace ProxyScraper.ScrapProxies;
internal class ParseIpAdresses
{
    private static readonly string _regexPatern = @"\b(?:\d{1,3}\.){3}\d{1,3}:\d{1,5}\b";
    public List<string> GetProxy(List<string> webPages)
    {
        List<string> result = new List<string>();

        foreach (string webPage in webPages)
        {
            result.AddRange(ParseFromPage(webPage));
        }
        return result.Distinct().ToList();
    }

    private List<string> ParseFromPage(string page)
    {
        Regex regex = new Regex(_regexPatern);
        MatchCollection matches = regex.Matches(page);
        List<string> ipAdresses = new List<string>();

        foreach (Match match in matches)
        {
            ipAdresses.Add(match.Value);
        }
        return ipAdresses;
    }
}
