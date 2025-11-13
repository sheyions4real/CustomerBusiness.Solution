using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerBusiness.Solution.Data.Models
{
    public class Business
    {
        [Key]
        public Guid BusinessId { get; set; }

        [Required]
        public Guid TenantId { get; set; }
        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(200)]
        public string Website { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }

        // Navigation
        public ICollection<Customer> Customers { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
