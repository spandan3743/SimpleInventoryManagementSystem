namespace SimpleInventoryManagementSystem.Models;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int MinimumStockLevel { get; set; }

    public decimal StockValue => Price * Quantity;
    public bool IsLowStock => Quantity <= MinimumStockLevel;
}
