using System.Text.Json.Serialization;

namespace SimpleInventoryManagementSystem.Models;

/// <summary>
/// Represents a product stored in the inventory.
/// Product inherits the common inventory behaviour from InventoryItem.
/// </summary>
public class Product : InventoryItem
{
    public string Category { get; private set; }
    public int MinimumStockLevel { get; private set; }

    [JsonConstructor]
    public Product(
        int productId,
        string productName,
        string category,
        decimal price,
        int quantity,
        int minimumStockLevel)
        : base(productId, productName, price, quantity)
    {
        if (minimumStockLevel < 0)
            throw new ArgumentOutOfRangeException(
                nameof(minimumStockLevel),
                "Minimum stock level cannot be negative.");

        Category = category?.Trim() ?? string.Empty;
        MinimumStockLevel = minimumStockLevel;
    }

    [JsonIgnore]
    public decimal StockValue => CalculateStockValue();

    [JsonIgnore]
    public bool IsLowStock => Quantity <= MinimumStockLevel;

    public override decimal CalculateStockValue() => Price * Quantity;

    public override string GetStockStatus() => IsLowStock ? "Low Stock" : "In Stock";

    public void UpdateDetails(
        string productName,
        string category,
        decimal price,
        int minimumStockLevel)
    {
        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required.", nameof(productName));

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        if (minimumStockLevel < 0)
            throw new ArgumentOutOfRangeException(
                nameof(minimumStockLevel),
                "Minimum stock level cannot be negative.");

        ProductName = productName.Trim();
        Category = category?.Trim() ?? string.Empty;
        Price = price;
        MinimumStockLevel = minimumStockLevel;
    }
}
