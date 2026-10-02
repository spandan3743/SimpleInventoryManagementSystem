namespace SimpleInventoryManagementSystem.Models;

/// <summary>
/// Manages the collection of products and the main inventory operations.
/// </summary>
public class Inventory
{
    private readonly List<Product> products = new();

    // Exposes a read-only view so callers cannot replace or directly modify the collection.
    public IReadOnlyList<Product> Products => products.AsReadOnly();

    public bool AddProduct(Product product)
    {
        if (FindById(product.ProductId) != null)
            return false;

        products.Add(product);
        return true;
    }

    public Product? FindById(int id) =>
        products.FirstOrDefault(p => p.ProductId == id);

    public IEnumerable<Product> Search(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return products;

        return products.Where(p =>
            p.ProductId.ToString().Contains(text, StringComparison.OrdinalIgnoreCase) ||
            p.ProductName.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

    public bool DeleteProduct(int id)
    {
        var product = FindById(id);
        if (product == null)
            return false;

        products.Remove(product);
        return true;
    }

    // The products are treated as InventoryItem objects here. Calling the overridden
    // CalculateStockValue method demonstrates polymorphic behaviour.
    public IEnumerable<InventoryItem> GetInventoryItems() => products;

    public decimal TotalValue =>
        GetInventoryItems().Sum(item => item.CalculateStockValue());
}
