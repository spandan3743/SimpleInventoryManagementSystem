# Manual Testing Checklist

| # | Scenario | Test data / action | Expected result |
|---|---|---|---|
| 1 | Add valid product | ID 101, Laptop Stand, Accessories, 39.95, Qty 10, Min 3 | Product is added, saved, displayed, and total value increases by $399.50. |
| 2 | Duplicate ID | Add another product with ID 101 | Message states that the ID already exists; duplicate is not added. |
| 3 | Empty product name | Leave Product Name blank | Validation message appears and product is not added. |
| 4 | Invalid ID | ID 0 or text such as ABC | Validation message requires a positive whole number. |
| 5 | Negative price | Price -10 | Validation message appears and product is not added. |
| 6 | Negative quantity | Quantity -1 | Validation message appears and product is not added. |
| 7 | Low-stock identification | ID 102, Mouse, price 25, Qty 2, Min 5 | Product displays `Low Stock`. |
| 8 | Search | Search for `101` and then `Laptop` | Matching product is shown in the grid. |
| 9 | Update product | Select ID 101 and change price to 45.00 | Product details are updated and stock value becomes $450.00 when Qty is 10. |
| 10 | Update stock | ID 102, change quantity from 2 to 8 | Stock is updated and status changes from `Low Stock` to `In Stock`. |
| 11 | Delete product | Delete an existing ID | Product is removed, saved, and total value is recalculated. |
| 12 | Nonexistent product | Update/delete ID 9999 | `Product not found` message appears. |
| 13 | JSON persistence | Add products, close the application, reopen it | Previously saved products are loaded from `inventory.json`. |
| 14 | Corrupt JSON handling | Temporarily replace `inventory.json` with invalid JSON | A load warning appears and the program starts with an empty inventory instead of crashing. |
| 15 | Save error handling | Make the output folder/file unwritable and attempt a save | A save error is displayed and the application remains open. |

## Presentation test data

For the live demonstration, keep the data simple:

- Product 101 - Laptop Stand - Accessories - $39.95 - Quantity 10 - Minimum 3
- Product 102 - Wireless Mouse - Electronics - $25.00 - Quantity 2 - Minimum 5
- Product 103 - USB-C Cable - Electronics - $12.50 - Quantity 20 - Minimum 5

This data demonstrates normal stock, low stock, searching, updating, deleting, and total inventory value without requiring a long setup.
