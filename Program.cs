using FileTransferBatch.Services;
using FileTransferBatch.Utilities;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var service = new FileTransferService();

        Logger.Info("ファイル転送バッチ開始");
        service.Execute();
        Logger.Info("ファイル転送バッチ終了");
    }
}