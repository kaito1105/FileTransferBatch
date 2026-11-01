namespace FileTransferBatch.Utilities;

public static class Logger
{
    public static void Info(string message)
    {
        Write("INFO", message);
    }

    public static void Error(string message)
    {
        Write("ERROR", message);
    }

    private static void Write(string level, string message)
    {
        var baseFolder = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..")
        );
        var logFolder = Path.Combine(baseFolder, "logs");
        Directory.CreateDirectory(logFolder);

        var log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";

        File.AppendAllText(
            Path.Combine(logFolder, "batch.log"),
            log + Environment.NewLine
        );
    }
}