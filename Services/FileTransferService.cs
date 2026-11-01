namespace FileTransferBatch.Services;

public class FileTransferService
{
    private readonly string _inputFolder = "input";
    private readonly string _destinationFolder = "destination";
    private readonly string _processedFolder = "processed";
    private readonly string _errorFolder = "error";

    public void Execute()
    {
        var files = Directory.GetFiles(_inputFolder);

        foreach (var file in files)
        {
            try
            {
                var fileName = Path.GetFileName(file);
                var destinationPath = Path.Combine(_destinationFolder, fileName);
                var processedPath = Path.Combine(_processedFolder, fileName);

                Console.WriteLine($"転送開始: {fileName}");

                // 転送
                File.Copy(file, destinationPath, true);

                // 成功したファイルをprocessedへ移動
                File.Move(file, processedPath, true);

                Console.WriteLine($"転送成功: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"転送失敗: {ex.Message}");
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