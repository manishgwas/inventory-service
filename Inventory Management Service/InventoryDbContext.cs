using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_Service
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        // Define DbSets for your entities here, e.g.:
        // public DbSet<Item> Items { get; set; }
    }
}
