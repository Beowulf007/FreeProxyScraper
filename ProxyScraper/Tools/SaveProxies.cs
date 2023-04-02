namespace ProxyScrapper.Tools;
internal class SaveProxies
{
    public void Save(string text, bool isChecked)
    {
        string proxyFilePath = GetProxyFilePath(isChecked);
        CheckDirectoryIsExist(Path.GetDirectoryName(proxyFilePath));

        using (StreamWriter writer = File.CreateText(proxyFilePath)) 
        {
            writer.WriteLine(text);
        }
    }
    private string GetProxyFilePath(bool isChecked) 
    {
        string subFolderName;
        if (isChecked) subFolderName = "checked";
        else subFolderName = "unchecked";

        return Path.Combine(Environment.CurrentDirectory, $"downloaded-proxies/{subFolderName}/{DateTime.Now.ToString("yyyy.MM.dd - HH.mm.ss")}.txt");
    }

    private void CheckDirectoryIsExist(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }
}
