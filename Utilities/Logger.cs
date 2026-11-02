namespace FileTransferBatch.Utilities;

public static class Logger
{
    public static DateTime Info(string message)
    {
        return Write("INFO", message);
    }

    public static DateTime Error(string message)
    {
        return Write("ERROR", message);
    }

    private static DateTime Write(string level, string message)
    {
        var baseFolder = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..")
        );
        var logFolder = Path.Combine(baseFolder, "logs");
        Directory.CreateDirectory(logFolder);

        DateTime now = DateTime.Now;
        var log = $"{now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

        File.AppendAllText(
            Path.Combine(logFolder, "batch.log"),
            log + Environment.NewLine
        );

        return now;
    }
}