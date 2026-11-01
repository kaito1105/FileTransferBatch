namespace FileTransferBatch.Models;

public class TransferResult
{
    public string FileName { get; set; } = "";
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = "";
    public DateTime ProcessedAt { get; set; }
}