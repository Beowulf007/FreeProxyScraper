using ProxyScraper.CheckProxies;
using ProxyScraper.ScrapProxies;
using ProxyScrapper.Tools;
using System.Diagnostics;

internal class Program
{
    private async static Task Main(string[] args)
    {
        string infoMessage = String.Empty;

        Console.WriteLine("İndirme başlatıldı!");
        var startTime = Stopwatch.GetTimestamp();
        var list = await DownloadPages.Download();
        var endTime = Stopwatch.GetTimestamp();
        var diff = Stopwatch.GetElapsedTime(startTime, endTime);

        infoMessage += String.Format($"Toplam {list.Count} sayfa indirildi. Geçen süre: {diff}\n");

        ParseIpAdresses parse = new ParseIpAdresses();

        startTime = Stopwatch.GetTimestamp();
        var proxyList = parse.GetProxy(list);
        endTime = Stopwatch.GetTimestamp();
        diff = Stopwatch.GetElapsedTime(startTime, endTime);

        infoMessage += String.Format($"Toplam {proxyList.Count} proxy ayıklandı. Geçen süre: {diff}\n");

        SaveProxies saveproxy = new SaveProxies();
        saveproxy.Save(String.Join(Environment.NewLine,proxyList),false);


        Thread.Sleep(TimeSpan.FromSeconds(2));
        Console.WriteLine(infoMessage);
        Console.WriteLine("İndiriler proxy'ler kaydedildi!");
        Console.WriteLine("Proxy kontrolü tamamen deneyseldir ve henüz tamamlanmamıştır sadece HTTP kontrol eder.\nProxy sayısı göz önüne alındığında oldukça uzun sürmesi de mümkündür.");
        
        if (AskCheckProxies())
        {
            startTime = Stopwatch.GetTimestamp();
            var checkedProxiesList = await CheckProxy.GetList(proxyList);
            endTime = Stopwatch.GetTimestamp();
            diff = Stopwatch.GetElapsedTime(startTime, endTime);
            Console.WriteLine($"Toplam {checkedProxiesList.Count} proxy kontrol edildi. Geçen süre: {diff}");
            saveproxy.Save(String.Join(Environment.NewLine, checkedProxiesList.Where(x => x.IsAlive).Select(x => x.FullAddress)), true);
        }
        else 
        {
            Console.WriteLine("Program sonlandırılıyor!");
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }
    }

    private static bool AskCheckProxies()
    {
        bool check = false;
        string input = String.Empty;
        do
        {
            Console.WriteLine("Proxy'leri kontrol etmek isterseniz 'e' istemiyorsanız 'h' tuşlayın: ");
            input = Console.ReadLine().ToLower();
            check = input == "e" || input == "h";
            if (!check)
            {
                Console.WriteLine("Hatalı tuşlama yaptınız!");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.Clear();
            }

        }while(!check);

        return input == "e";
    }
}