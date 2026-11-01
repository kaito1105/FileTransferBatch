namespace FileTransferBatch.Services;

public class FileValidator
{
    public bool Validate(string filePath)
    {
        var extension = Path.GetExtension(filePath);

        if (extension != ".csv")
        {
            return false;
        }
        return true;
    }
}