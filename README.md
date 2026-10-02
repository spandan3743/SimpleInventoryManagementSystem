# Simple Inventory Management System

**ITS203 - Object-Oriented Design and Programming - Assessment C**  
**Student:** Spandan Dahal  
**Student ID:** S2501291

## Project description

The Simple Inventory Management System is a C# Windows Forms desktop application designed for a small business that needs a simple way to manage product and stock information. It allows the user to add, view, search, update and delete products, update stock quantities, identify low-stock products, calculate stock values, validate user input and save inventory information to a JSON file so that data can be loaded again after the application is restarted.

The project was developed as a practical demonstration of object-oriented programming (OOP) concepts covered in ITS203.

## Main features

- Add a new product using a unique Product ID
- View all products in a DataGridView
- Search by Product ID or Product Name
- Update product details
- Delete an existing product
- Update stock quantity
- Identify products as `Low Stock` or `In Stock`
- Calculate the stock value of each product
- Calculate the total inventory value
- Validate invalid or incomplete user input
- Prevent duplicate Product IDs
- Save inventory data to `inventory.json`
- Load saved inventory data when the program starts
- Handle file/JSON errors with exception handling instead of allowing the program to crash

## OOP design

The final application demonstrates the required OOP principles:

- **Classes and objects:** `Product`, `Inventory`, `InventoryItem`, `StorageException`, `JsonStorage`, and `Form1` divide the program into clear responsibilities.
- **Encapsulation:** product values use controlled properties and update methods. The `Inventory` class stores products in a private list and exposes a read-only collection.
- **Inheritance:** `Product` inherits from the abstract `InventoryItem` base class.
- **Abstraction:** `InventoryItem` defines common inventory data and abstract behaviours such as `CalculateStockValue()` and `GetStockStatus()`.
- **Polymorphism:** `Product` overrides the abstract methods. `Inventory.TotalValue` operates through `InventoryItem` references and calls the overridden `CalculateStockValue()` implementation.
- **Exception handling:** `JsonStorage` catches file, permission and JSON errors and throws a `StorageException`; `Form1` catches this exception and displays a user-friendly message.

## Project structure

```text
SimpleInventoryManagementSystem/
|-- Documentation/
|   `-- Milestone 1 - Project Idea Proposal.docx
|-- Models/
|   |-- Inventory.cs
|   |-- InventoryItem.cs
|   `-- Product.cs
|-- Services/
|   |-- JsonStorage.cs
|   `-- StorageException.cs
|-- Form1.cs
|-- Program.cs
|-- SimpleInventoryManagementSystem.csproj
|-- SimpleInventoryManagementSystem.slnx
|-- TESTING.md
|-- README.md
|-- .gitignore
`-- .gitattributes
```

## Requirements

- Windows 10 or Windows 11
- Visual Studio 2022 or later with the **.NET desktop development** workload installed
- .NET 8 SDK

## How to run the application

Download or clone this repository.
Open SimpleInventoryManagementSystem.slnx in Microsoft Visual Studio.
If the .slnx file cannot be opened, open SimpleInventoryManagementSystem.csproj.
Make sure the .NET desktop development workload is installed.
Build the project using Build > Rebuild Solution.
Press F5 or select Debug > Start Debugging.
The Simple Inventory Management System window will open.

## Setup notes

No database or external server is required. The program uses `System.Text.Json`, which is included with .NET 8. The JSON file is created automatically after the first successful save.

## Testing

A final manual test checklist and suggested presentation test data are provided in [`TESTING.md`](TESTING.md). The tests cover valid data, invalid input, duplicate IDs, low-stock logic, searching, updating, deleting, stock calculations, JSON persistence and exception handling.

## Milestone 1 proposal

The approved Milestone 1 proposal is included in the repository at:

`Documentation/Milestone 1 - Project Idea Proposal.docx`

## GitHub repository

https://github.com/spandan3743/SimpleInventoryManagementSystem

## References and tools used

Microsoft. (2025). *C# classes*. Microsoft Learn.  
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/classes

Microsoft. (2025). *Object-oriented programming (C#)*. Microsoft Learn.  
https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/oop

Microsoft. (2025). *How to serialize and deserialize JSON in .NET*. Microsoft Learn.  
https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview

Microsoft. (2026). *Windows Forms overview*. Microsoft Learn.  
https://learn.microsoft.com/en-us/dotnet/desktop/winforms/overview/

ChataGpt was used only to support idea development, clarify concepts, and assist with debugging. The final project was reviewed and tested and it ensures my own work and reflects my understanding.
