using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_Service
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<SKU> SKUs { get; set; } = new List<SKU>();
        [Timestamp]
        public byte[] RowVersion { get; set; } = new byte[8];
    }
}
