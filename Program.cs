using Microsoft.Extensions.Configuration;
using FileTransferBatch.Models;
using FileTransferBatch.Services;
using FileTransferBatch.Utilities;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var baseFolder = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..")
        );

        var configuration = new ConfigurationBuilder()
            .SetBasePath(baseFolder)
            .AddJsonFile("appsettings.json")
            .Build();

        var settings = configuration
            .GetSection("FileTransfer")
            .Get<FileTransferSettings>();

        if (settings == null)
        {
            throw new Exception("FileTransferの設定を読み込めませんでした。");
        }

        var service = new FileTransferService(settings, baseFolder);

        Logger.Info("ファイル転送バッチ開始");
        service.Execute();
        Logger.Info("ファイル転送バッチ終了");
    }
}