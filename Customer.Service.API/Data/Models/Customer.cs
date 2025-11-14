using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerBusiness.Solution.Data.Models
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid BusinessId { get; set; }
        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }

        // Navigation
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
