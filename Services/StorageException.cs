namespace SimpleInventoryManagementSystem.Services;

/// <summary>
/// Represents an error that occurs while inventory data is being saved or loaded.
/// </summary>
public class StorageException : Exception
{
    public StorageException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
