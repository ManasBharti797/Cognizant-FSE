using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailInventory.Data;
using RetailInventory.Models;

namespace RetailInventory
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            using (var context = new AppDbContext())
            {
                try
                {
                    await context.Database.EnsureCreatedAsync();
                    Console.WriteLine("Database created successfully!\n");
                    
                    if (!await context.Categories.AnyAsync())
                    {
                        Console.WriteLine("Adding sample data...");
                        
                        var electronics = new Category { Name = "Electronics" };
                        var clothing = new Category { Name = "Clothing" };
                        var groceries = new Category { Name = "Groceries" };
                        
                        await context.Categories.AddRangeAsync(electronics, clothing, groceries);
                        await context.SaveChangesAsync();
                        
                        // Add products
                        var products = new[]
                        {
                            new Product { Name = "Laptop", Price = 59999.99m, CategoryId = electronics.Id },
                            new Product { Name = "Smartphone", Price = 19999.99m, CategoryId = electronics.Id },
                            new Product { Name = "T-Shirt", Price = 399.99m, CategoryId = clothing.Id },
                            new Product { Name = "Jeans", Price = 499.99m, CategoryId = clothing.Id },
                            new Product { Name = "Apples (1kg)", Price = 103.99m, CategoryId = groceries.Id },
                            new Product { Name = "Bread", Price = 50.49m, CategoryId = groceries.Id }
                        };
                        
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync();
                        
                        Console.WriteLine("Sample data added successfully!\n");
                    }
                    
                    Console.WriteLine("Current Inventory:");
                    Console.WriteLine("-----------------");
                    
                    var productsWithCategories = await context.Products
                        .Include(p => p.Category)
                        .OrderBy(p => p.Category != null ? p.Category.Name : string.Empty)
                        .ThenBy(p => p.Name)
                        .ToListAsync();
                    
                    foreach (var product in productsWithCategories)
                    {
                        string categoryName = product.Category?.Name ?? "Uncategorized";
                        Console.WriteLine($"{product.Name} - ₹{product.Price} (Category: {categoryName})");
                    }
                    
                    Console.WriteLine("\nLab 4: Inserting Additional Data");
                    Console.WriteLine("------------------------------");
                    
                    if (!await context.Products.AnyAsync(p => p.Name == "Rice Bag"))
                    {
                        var existingElectronics = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Electronics");
                        var existingGroceries = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Groceries");
                        
                        if (existingElectronics == null)
                        {
                            existingElectronics = new Category { Name = "Electronics" };
                            await context.Categories.AddAsync(existingElectronics);
                            await context.SaveChangesAsync();
                        }
                        
                        if (existingGroceries == null)
                        {
                            existingGroceries = new Category { Name = "Groceries" };
                            await context.Categories.AddAsync(existingGroceries);
                            await context.SaveChangesAsync();
                        }
                        
                        var newLaptop = new Product { Name = "Gaming Laptop", Price = 75000, Category = existingElectronics };
                        var riceBag = new Product { Name = "Rice Bag", Price = 1200, Category = existingGroceries };
                        
                        await context.Products.AddRangeAsync(newLaptop, riceBag);
                        await context.SaveChangesAsync();
                        
                        Console.WriteLine("New products added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Additional products already exist in the database.");
                    }
                    
                    Console.WriteLine("\nLab 5: Retrieving Data from the Database");
                    Console.WriteLine("-------------------------------------");
                    
                    Console.WriteLine("\nAll Products:");
                    var allProducts = await context.Products.ToListAsync();
                    foreach (var p in allProducts)
                    {
                        Console.WriteLine($"{p.Name} - ₹{p.Price}");
                    }
                    
                    Console.WriteLine("\nFind by ID (1):");
                    var foundProduct = await context.Products.FindAsync(1);
                    Console.WriteLine($"Found: {foundProduct?.Name}");
                    
                    Console.WriteLine("\nExpensive Products (Price > ₹500):");
                    var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 500);
                    Console.WriteLine($"Expensive: {expensive?.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                }
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
