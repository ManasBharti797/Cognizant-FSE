using Microsoft.EntityFrameworkCore;
using RetailInventory.Models;
using System.IO;

namespace RetailInventory.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQLite with a local database file
            string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "RetailInventory.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}