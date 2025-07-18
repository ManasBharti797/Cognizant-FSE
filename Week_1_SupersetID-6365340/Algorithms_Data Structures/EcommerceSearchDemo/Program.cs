using System;
using System.Diagnostics;

public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Category { get; set; }

    public Product(int id, string name, string category)
    {
        ProductId = id;
        ProductName = name;
        Category = category;
    }

    public override string ToString()
    {
        return $"{ProductId} - {ProductName} ({Category})";
    }
}

class Program
{
    public static Product LinearSearch(Product[] products, string name)
    {
        foreach (var product in products)
        {
            if (product.ProductName.Equals(name, StringComparison.OrdinalIgnoreCase))
                return product;
        }
        return null;
    }

    public static Product BinarySearch(Product[] products, string name)
    {
        int left = 0;
        int right = products.Length - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int comparison = string.Compare(products[mid].ProductName, name, true);
            if (comparison == 0)
                return products[mid];
            else if (comparison < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return null;
    }

    static void Main()
    {
        int n = 100000;
        int trials = 1000;
        string searchName = "Product99999";

        Product[] products = new Product[n];
        for (int i = 0; i < n; i++)
        {
            products[i] = new Product(i, "Product" + i, "Category" + (i % 10));
        }

        Array.Sort(products, (p1, p2) => p1.ProductName.CompareTo(p2.ProductName));

        LinearSearch(products, searchName);
        BinarySearch(products, searchName);

        double totalLinear = 0;
        double totalBinary = 0;

        for (int i = 0; i < trials; i++)
        {
            var sw1 = Stopwatch.StartNew();
            var result1 = LinearSearch(products, searchName);
            sw1.Stop();
            totalLinear += sw1.Elapsed.TotalMilliseconds;

            var sw2 = Stopwatch.StartNew();
            var result2 = BinarySearch(products, searchName);
            sw2.Stop();
            totalBinary += sw2.Elapsed.TotalMilliseconds;
        }

        Console.WriteLine($"After {trials} trials on {n} items:");
        Console.WriteLine("Average Linear Search Time: " + (totalLinear / trials) + " ms");
        Console.WriteLine("Average Binary Search Time: " + (totalBinary / trials) + " ms");

        var finalLinear = LinearSearch(products, searchName);
        var finalBinary = BinarySearch(products, searchName);

        Console.WriteLine("\nSample Result:");
        Console.WriteLine("Linear: " + (finalLinear != null ? finalLinear.ToString() : "Not found"));
        Console.WriteLine("Binary: " + (finalBinary != null ? finalBinary.ToString() : "Not found"));
    }
}
