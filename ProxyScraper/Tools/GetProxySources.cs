namespace ProxyScrapper.Tools;
internal static class GetProxySources
{
    private static string _path = AppDomain.CurrentDomain.BaseDirectory + "sources.txt";
    public static List<string> GetSources()
    {
        if (!File.Exists(_path))
        {
            throw new FileNotFoundException($"Dosya bulunamadı: {_path}");
        }
        return File.ReadAllLines(_path).ToList();
    }
}
