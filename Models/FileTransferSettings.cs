namespace FileTransferBatch.Models;

public class FileTransferSettings
{
    public string InputFolder { get; set; } = "";
    public string DestinationFolder { get; set; } = "";
    public string ProcessedFolder { get; set; } = "";
    public string ErrorFolder { get; set; } = "";
}