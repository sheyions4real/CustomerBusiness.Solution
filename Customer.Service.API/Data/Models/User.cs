using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Service.API.Data.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }
        [ForeignKey("TenantId")]
        public Tenant Tenant { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; }

        [MaxLength(150)]
        public string Email { get; set; }

        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        public string Role { get; set; }

        [MaxLength(50)]
        public string Provider { get; set; }

        [MaxLength(150)]
        public string ProviderUserId { get; set; }

        public bool IsEmailVerified { get; set; } = false;

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }

        public ICollection<LoginToken> LoginTokens { get; set; }
    }
}
