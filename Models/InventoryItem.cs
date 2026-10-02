namespace SimpleInventoryManagementSystem.Models;

/// <summary>
/// Defines the common data and behaviour shared by inventory items.
/// This abstract class demonstrates abstraction and provides a base type
/// for polymorphic inventory operations.
/// </summary>
public abstract class InventoryItem
{
    public int ProductId { get; protected set; }
    public string ProductName { get; protected set; } = string.Empty;
    public decimal Price { get; protected set; }
    public int Quantity { get; protected set; }

    protected InventoryItem(int productId, string productName, decimal price, int quantity)
    {
        if (productId <= 0)
            throw new ArgumentOutOfRangeException(nameof(productId), "Product ID must be positive.");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required.", nameof(productName));

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");

        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative.");

        ProductId = productId;
        ProductName = productName.Trim();
        Price = price;
        Quantity = quantity;
    }

    public virtual void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative.");

        Quantity = quantity;
    }

    public abstract decimal CalculateStockValue();
    public abstract string GetStockStatus();
}
