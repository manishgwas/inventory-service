using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_Service
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
