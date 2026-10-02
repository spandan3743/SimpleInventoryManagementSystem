using System.Text.Json;
using SimpleInventoryManagementSystem.Models;

namespace SimpleInventoryManagementSystem.Services;

/// <summary>
/// Saves and loads inventory data in JSON format.
/// </summary>
public static class JsonStorage
{
    private static readonly string FilePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventory.json");

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    public static void Save(Inventory inventory)
    {
        try
        {
            var json = JsonSerializer.Serialize(inventory.Products, JsonOptions);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex) when (
            ex is IOException or
            UnauthorizedAccessException or
            NotSupportedException)
        {
            throw new StorageException("The inventory data could not be saved.", ex);
        }
    }

    public static Inventory Load()
    {
        var inventory = new Inventory();

        if (!File.Exists(FilePath))
            return inventory;

        try
        {
            var json = File.ReadAllText(FilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(json, JsonOptions) ?? new();

            foreach (var product in products)
                inventory.AddProduct(product);

            return inventory;
        }
        catch (Exception ex) when (
            ex is IOException or
            UnauthorizedAccessException or
            JsonException or
            NotSupportedException or
            ArgumentException)
        {
            throw new StorageException("The inventory data could not be loaded.", ex);
        }
    }
}
