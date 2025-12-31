using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Service.API.Data.Models
{
    public class Sale
    {
        [Key]
        public Guid SaleId { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Guid BusinessId { get; set; }
        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string Status { get; set; } = "Completed";

        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
