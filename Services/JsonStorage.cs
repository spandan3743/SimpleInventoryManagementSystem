using System.Text.Json;
using SimpleInventoryManagementSystem.Models;

namespace SimpleInventoryManagementSystem.Services;

public static class JsonStorage
{
    private static readonly string FilePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inventory.json");

    public static void Save(Inventory inventory)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(FilePath, JsonSerializer.Serialize(inventory.Products, options));
    }

    public static Inventory Load()
    {
        var inventory = new Inventory();

        if (!File.Exists(FilePath))
            return inventory;

        try
        {
            var json = File.ReadAllText(FilePath);
            var products = JsonSerializer.Deserialize<List<Product>>(json) ?? new();
            foreach (var product in products)
                inventory.AddProduct(product);
        }
        catch
        {
            // Start with an empty inventory if the file cannot be read.
        }

        return inventory;
    }
}
