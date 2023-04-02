namespace ProxyScrapper.Tools;
internal static class Logger
{
    private static readonly string _logFilePath = Path.Combine(Environment.CurrentDirectory, $"logs/log - {DateTime.Now.ToString("yyyy.MM.dd - HH.mm.ss")}.txt");
    private static readonly string _directoryPath;

    static Logger()
    {
        _directoryPath = Path.GetDirectoryName(_logFilePath);
    }
    private static object _locker = new object();
    public static void Log(string message)
    {
        lock (_locker)
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            DateTime now = DateTime.Now;
            string logMessage = $"{now.ToString()} - {message}";

            using (StreamWriter writer = new StreamWriter(_logFilePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
    }
}