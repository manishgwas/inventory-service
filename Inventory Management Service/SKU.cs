using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_Service
{
    public class SKU
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        // Add additional SKU properties as needed
    }
}
