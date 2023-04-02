namespace ProxyScraper.CheckProxies.Models;
internal class Proxy
{
    public string FullAddress { get; set; }
    public string IpAddress { get; set; }
    public string Port { get; set; }
    public int Latency { get; set; }
    public bool IsAlive { get; set; }
    public DateTime LastCheckTime { get; set; }

}
