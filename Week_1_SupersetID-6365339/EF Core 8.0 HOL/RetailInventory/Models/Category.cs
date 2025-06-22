using System;
using System.Collections.Generic;

namespace RetailInventory.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }
        
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}