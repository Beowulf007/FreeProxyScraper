using ProxyScraper.CheckProxies.Models;
using System;
using System.Diagnostics;
using System.Net;

namespace ProxyScraper.CheckProxies;
internal static class CheckProxy
{

    public async static Task<List<Proxy>> GetList(List<string> proxyList)
    {
        var taskList = new List<Task<Proxy>>();
        proxyList.ForEach(proxy => {
            taskList.Add(Check(proxy));
        });
        var result = await Task.WhenAll(taskList.ToArray());
        return result.ToList();
    }

    public static async Task<Proxy> Check(string proxyString)
    {
        var proxy = new Proxy();
        var webProxy = new WebProxy($"http://{proxyString}");

        proxy.FullAddress = proxyString;

        var proxyArray = proxyString.Split(":");

        proxy.IpAddress = proxyArray[0];
        proxy.Port = proxyArray[1];

        var handler = new HttpClientHandler { Proxy = webProxy };
        var client = new HttpClient(handler);
        client.Timeout = TimeSpan.FromSeconds(30);

        var startTime = Stopwatch.GetTimestamp();
        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
        try
        {
            response = await client.GetAsync("https://www.google.com/");
        }
        catch (Exception ex) { };


        var endTime = Stopwatch.GetTimestamp();
        var diff = Stopwatch.GetElapsedTime(startTime, endTime);
        proxy.IsAlive = response.IsSuccessStatusCode;
        proxy.Latency = diff.Milliseconds;
        proxy.LastCheckTime = DateTime.Now;


        await Console.Out.WriteLineAsync($"{proxy.FullAddress} kontrol edildi.");
        return proxy;
    }
}
