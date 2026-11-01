using FileTransferBatch.Services;

public class Program
{
    public static void Main()
    {
        var service = new FileTransferService();

        Console.WriteLine("ファイル転送バッチを開始します。");
        service.Execute();
        Console.WriteLine("ファイル転送バッチを終了します。");
    }
}