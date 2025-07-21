
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Inventory_Management_Service
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Category
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product
            modelBuilder.Entity<Product>()
                .HasMany(p => p.SKUs)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // SKU unique constraint
            modelBuilder.Entity<SKU>()
                .HasIndex(s => s.Code)
                .IsUnique();

            // Unique product name within a category
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Name, p.CategoryId })
                .IsUnique();

            // Property constraints
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<SKU>()
                .Property(s => s.Code)
                .IsRequired()
                .HasMaxLength(50);

            // Seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", CategoryId = 1 },
                new Product { Id = 3, Name = "T-Shirt", CategoryId = 2 }
            );

            modelBuilder.Entity<SKU>().HasData(
                new SKU { Id = 1, Code = "ELEC-001", ProductId = 1 },
                new SKU { Id = 2, Code = "ELEC-002", ProductId = 2 },
                new SKU { Id = 3, Code = "CLOTH-001", ProductId = 3 }
            );
        }
    }
}
