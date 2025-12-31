using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Service.API.Data.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }
        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        [MaxLength(100)]
        public string SKU { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
