using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerBusiness.Solution.Data.Models
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ScheduleId { get; set; }

        [ForeignKey("ScheduleId")]
        public Schedule Schedule { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        public string CustomerName { get; set; } = null!;

        [MaxLength(100)]
        public string? CustomerEmail { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

}
