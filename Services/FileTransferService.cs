namespace FileTransferBatch.Services;

using FileTransferBatch.Utilities;

public class FileTransferService
{
    private readonly string baseFolder;
    private readonly string inputFolder;
    private readonly string destinationFolder;
    private readonly string processedFolder;
    private readonly string errorFolder;

    public FileTransferService()
    {
        baseFolder = Path.GetFullPath(
            Path.Combine(AppContext.BaseDirectory, "..", "..", "..")
        );
        inputFolder = Path.Combine(baseFolder, "input");
        destinationFolder = Path.Combine(baseFolder, "destination");
        processedFolder = Path.Combine(baseFolder, "processed");
        errorFolder = Path.Combine(baseFolder, "error");

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
            try
            {
                var fileName = Path.GetFileName(file);
                bool isValid = new FileValidator().Validate(file);

                if (!isValid)
                {
                    Logger.Error($"無効なファイル形式（転送失敗）: {fileName}");
                    MoveToError(file);
                    continue;
                }

                var destinationPath = Path.Combine(destinationFolder, fileName);
                var processedPath = Path.Combine(processedFolder, fileName);

                Logger.Info($"転送開始: {fileName}");

                // 転送
                File.Copy(file, destinationPath, true);

                // 成功したファイルをprocessedへ移動
                File.Move(file, processedPath, true);

                Logger.Info($"転送成功: {fileName}");
            }
            catch (Exception ex)
            {
                Logger.Error($"転送失敗: {ex.Message}");
                MoveToError(file);
            }
        }
    }

    private void MoveToError(string file)
    {
        var fileName = Path.GetFileName(file);
        var errorPath = Path.Combine(errorFolder, fileName);

        File.Move(file, errorPath, true);
    }
}