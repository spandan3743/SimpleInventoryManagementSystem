namespace SimpleInventoryManagementSystem.Models;

public class Inventory
{
    public List<Product> Products { get; private set; } = new();

    public bool AddProduct(Product product)
    {
        if (Products.Any(p => p.ProductId == product.ProductId))
            return false;

        Products.Add(product);
        return true;
    }

    public Product? FindById(int id) =>
        Products.FirstOrDefault(p => p.ProductId == id);

    public IEnumerable<Product> Search(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Products;

        return Products.Where(p =>
            p.ProductId.ToString().Contains(text, StringComparison.OrdinalIgnoreCase) ||
            p.ProductName.Contains(text, StringComparison.OrdinalIgnoreCase));
    }

    public bool DeleteProduct(int id)
    {
        var product = FindById(id);
        if (product == null) return false;
        Products.Remove(product);
        return true;
    }

    public decimal TotalValue => Products.Sum(p => p.StockValue);
}
