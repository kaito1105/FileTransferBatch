namespace FileTransferBatch.Services;

using FileTransferBatch.Utilities;
using FileTransferBatch.Models;

public class FileTransferService
{
    // private readonly string baseFolder;
    private readonly string inputFolder;
    private readonly string destinationFolder;
    private readonly string processedFolder;
    private readonly string errorFolder;
    private readonly List<TransferResult> transferResults;
    private DateTime now;

    public FileTransferService(FileTransferSettings settings, string baseFolder)
    {
        transferResults = new List<TransferResult>();
        inputFolder = Path.Combine(baseFolder, settings.InputFolder);
        destinationFolder = Path.Combine(baseFolder, settings.DestinationFolder);
        processedFolder = Path.Combine(baseFolder, settings.ProcessedFolder);
        errorFolder = Path.Combine(baseFolder, settings.ErrorFolder);

        // フォルダが存在しない場合は作成
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(destinationFolder);
        Directory.CreateDirectory(processedFolder);
        Directory.CreateDirectory(errorFolder);
    }

    public void Execute()
    {
        var files = Directory.GetFiles(inputFolder);

        if (files.Length == 0)
        {
            Logger.Info("転送ファイルなし");
            return;
        }

        foreach (var file in files)
        {
            var fileName = Path.GetFileName(file);
            Logger.Info($"転送開始: {fileName}");

            try
            {
                bool isValid = new FileValidator().Validate(file);

                if (!isValid)
                {
                    now = Logger.Error($"無効なファイル形式（転送失敗）: {fileName}");
                    AddTransferResult(fileName, false, "無効なファイル形式", now);
                    MoveToError(file);
                    continue;
                }

                var destinationPath = Path.Combine(destinationFolder, fileName);
                var processedPath = Path.Combine(processedFolder, fileName);

                // 転送
                File.Copy(file, destinationPath, true);

                // 成功したファイルをprocessedへ移動
                File.Move(file, processedPath, true);

                now = Logger.Info($"転送成功: {fileName}");
                AddTransferResult(fileName, true, "転送成功", now);
            }
            catch (Exception ex)
            {
                now = Logger.Error($"転送失敗: {ex.Message}");
                AddTransferResult(fileName, false, "転送失敗", now);
                MoveToError(file);
            }
        }

        // コンソールに転送結果を表示
        Console.WriteLine("転送結果:");
        foreach (var result in transferResults)
        {
            Console.WriteLine(
                $"{result.ProcessedAt:yyyy-MM-dd HH:mm:ss} - {result.FileName} - {(result.IsSuccess ? "成功" : "失敗")} - {result.Message}"
            );
        }
    }

    private void MoveToError(string file)
    {
        var fileName = Path.GetFileName(file);
        var errorPath = Path.Combine(errorFolder, fileName);

        File.Move(file, errorPath, true);
    }

    private void AddTransferResult(string fileName, bool isSuccess, string message, DateTime processedAt)
    {
        transferResults.Add(new TransferResult
        {
            FileName = fileName,
            IsSuccess = isSuccess,
            Message = message,
            ProcessedAt = processedAt
        });
    }
}