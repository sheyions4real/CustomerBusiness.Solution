using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerBusiness.Solution.Data.Models
{
    public class SaleItem
    {
        [Key]
        public Guid SaleItemId { get; set; }

        public Guid SaleId { get; set; }
        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Subtotal => Quantity * UnitPrice;
    }
}
