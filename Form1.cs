using SimpleInventoryManagementSystem.Models;
using SimpleInventoryManagementSystem.Services;

namespace SimpleInventoryManagementSystem;

public class Form1 : Form
{
    private readonly Inventory inventory;
    private readonly TextBox txtId = new();
    private readonly TextBox txtName = new();
    private readonly TextBox txtCategory = new();
    private readonly TextBox txtPrice = new();
    private readonly TextBox txtQuantity = new();
    private readonly TextBox txtMinimum = new();
    private readonly TextBox txtSearch = new();
    private readonly DataGridView grid = new();
    private readonly Label lblTotal = new();
    private readonly Label lblStatus = new();

    public Form1()
    {
        inventory = JsonStorage.Load();
        Text = "Simple Inventory Management System";
        Width = 1050;
        Height = 700;
        StartPosition = FormStartPosition.CenterScreen;
        BuildInterface();
        RefreshGrid(inventory.Products);
    }

    private void BuildInterface()
    {
        var title = new Label
        {
            Text = "Simple Inventory Management System",
            Font = new Font("Segoe UI", 18, FontStyle.Bold),
            AutoSize = true,
            Location = new Point(20, 15)
        };
        Controls.Add(title);

        AddLabel("Product ID", 20, 65); txtId.Location = new Point(140, 62);
        AddLabel("Product Name", 20, 100); txtName.Location = new Point(140, 97);
        AddLabel("Category", 20, 135); txtCategory.Location = new Point(140, 132);
        AddLabel("Price", 20, 170); txtPrice.Location = new Point(140, 167);
        AddLabel("Quantity", 20, 205); txtQuantity.Location = new Point(140, 202);
        AddLabel("Min Stock", 20, 240); txtMinimum.Location = new Point(140, 237);

        foreach (var box in new[] { txtId, txtName, txtCategory, txtPrice, txtQuantity, txtMinimum })
        {
            box.Width = 180;
            Controls.Add(box);
        }

        var btnAdd = MakeButton("Add Product", 350, 62, (_, _) => AddProduct());
        var btnUpdate = MakeButton("Update Product", 350, 102, (_, _) => UpdateProduct());
        var btnDelete = MakeButton("Delete Product", 350, 142, (_, _) => DeleteProduct());
        var btnStock = MakeButton("Update Stock", 350, 182, (_, _) => UpdateStock());
        var btnClear = MakeButton("Clear", 350, 222, (_, _) => ClearInputs());

        AddLabel("Search ID or Name", 570, 65);
        txtSearch.Location = new Point(700, 62);
        txtSearch.Width = 200;
        Controls.Add(txtSearch);
        var btnSearch = MakeButton("Search", 700, 102, (_, _) =>
            RefreshGrid(inventory.Search(txtSearch.Text)));
        var btnShowAll = MakeButton("Show All", 810, 102, (_, _) =>
            RefreshGrid(inventory.Products));

        grid.Location = new Point(20, 290);
        grid.Width = 990;
        grid.Height = 300;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.SelectionChanged += (_, _) => LoadSelectedProduct();
        Controls.Add(grid);

        lblTotal.Text = "Total Inventory Value: $0.00";
        lblTotal.Font = new Font("Segoe UI", 11, FontStyle.Bold);
        lblTotal.AutoSize = true;
        lblTotal.Location = new Point(20, 610);
        Controls.Add(lblTotal);

        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(20, 640);
        Controls.Add(lblStatus);
    }

    private void AddLabel(string text, int x, int y)
    {
        Controls.Add(new Label { Text = text, AutoSize = true, Location = new Point(x, y + 3) });
    }

    private Button MakeButton(string text, int x, int y, EventHandler handler)
    {
        var button = new Button { Text = text, Location = new Point(x, y), Width = 105, Height = 30 };
        button.Click += handler;
        Controls.Add(button);
        return button;
    }

    private bool TryReadProduct(out Product product)
    {
        product = new Product();

        if (!int.TryParse(txtId.Text, out int id) || id <= 0)
        {
            MessageBox.Show("Product ID must be a positive whole number.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Product name is required.");
            return false;
        }

        if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
        {
            MessageBox.Show("Price must be a valid non-negative number.");
            return false;
        }

        if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
        {
            MessageBox.Show("Quantity must be a non-negative whole number.");
            return false;
        }

        if (!int.TryParse(txtMinimum.Text, out int minimum) || minimum < 0)
        {
            MessageBox.Show("Minimum stock level must be a non-negative whole number.");
            return false;
        }

        product = new Product
        {
            ProductId = id,
            ProductName = txtName.Text.Trim(),
            Category = txtCategory.Text.Trim(),
            Price = price,
            Quantity = quantity,
            MinimumStockLevel = minimum
        };
        return true;
    }

    private void AddProduct()
    {
        if (!TryReadProduct(out var product)) return;

        if (!inventory.AddProduct(product))
        {
            MessageBox.Show("A product with that ID already exists.");
            return;
        }

        SaveAndRefresh();
        ClearInputs();
        lblStatus.Text = "Product added successfully.";
    }

    private void UpdateProduct()
    {
        if (!TryReadProduct(out var updated)) return;

        var existing = inventory.FindById(updated.ProductId);
        if (existing == null)
        {
            MessageBox.Show("Product not found.");
            return;
        }

        existing.ProductName = updated.ProductName;
        existing.Category = updated.Category;
        existing.Price = updated.Price;
        existing.Quantity = updated.Quantity;
        existing.MinimumStockLevel = updated.MinimumStockLevel;

        SaveAndRefresh();
        lblStatus.Text = "Product updated successfully.";
    }

    private void DeleteProduct()
    {
        if (!int.TryParse(txtId.Text, out int id))
        {
            MessageBox.Show("Enter a valid Product ID.");
            return;
        }

        if (inventory.DeleteProduct(id))
        {
            SaveAndRefresh();
            ClearInputs();
            lblStatus.Text = "Product deleted successfully.";
        }
        else MessageBox.Show("Product not found.");
    }

    private void UpdateStock()
    {
        if (!int.TryParse(txtId.Text, out int id) ||
            !int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
        {
            MessageBox.Show("Enter a valid Product ID and non-negative quantity.");
            return;
        }

        var product = inventory.FindById(id);
        if (product == null)
        {
            MessageBox.Show("Product not found.");
            return;
        }

        product.Quantity = quantity;
        SaveAndRefresh();
        LoadProductIntoInputs(product);
        lblStatus.Text = "Stock updated successfully.";
    }

    private void LoadSelectedProduct()
    {
        if (grid.CurrentRow?.DataBoundItem is Product product)
            LoadProductIntoInputs(product);
    }

    private void LoadProductIntoInputs(Product product)
    {
        txtId.Text = product.ProductId.ToString();
        txtName.Text = product.ProductName;
        txtCategory.Text = product.Category;
        txtPrice.Text = product.Price.ToString("0.00");
        txtQuantity.Text = product.Quantity.ToString();
        txtMinimum.Text = product.MinimumStockLevel.ToString();
    }

    private void ClearInputs()
    {
        txtId.Clear(); txtName.Clear(); txtCategory.Clear();
        txtPrice.Clear(); txtQuantity.Clear(); txtMinimum.Clear();
    }

    private void SaveAndRefresh()
    {
        JsonStorage.Save(inventory);
        RefreshGrid(inventory.Products);
    }

    private void RefreshGrid(IEnumerable<Product> products)
    {
        grid.DataSource = null;
        grid.DataSource = products.Select(p => new
        {
            p.ProductId,
            p.ProductName,
            p.Category,
            Price = p.Price.ToString("C"),
            p.Quantity,
            p.MinimumStockLevel,
            LowStock = p.IsLowStock ? "YES" : "No",
            StockValue = p.StockValue.ToString("C")
        }).ToList();

        lblTotal.Text = $"Total Inventory Value: {inventory.TotalValue:C}";
    }
}
