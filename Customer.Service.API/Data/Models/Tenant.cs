using System.ComponentModel.DataAnnotations;

namespace Customer.Service.API.Data.Models
{
    public class Tenant
    {
        [Key]
        public Guid TenantId { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Domain { get; set; }

        [MaxLength(150)]
        public string ContactEmail { get; set; }

        [MaxLength(50)]
        public string ContactPhone { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }

        // Navigation
        public ICollection<Business> Businesses { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
