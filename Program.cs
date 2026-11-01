using FileTransferBatch.Services;

public static class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        var service = new FileTransferService();

        Console.WriteLine("ファイル転送バッチを開始します。");
        service.Execute();
        Console.WriteLine("ファイル転送バッチを終了します。");
    }
}