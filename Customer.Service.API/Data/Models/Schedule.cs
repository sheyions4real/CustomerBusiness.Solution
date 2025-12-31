using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.Service.API.Data.Models
{
    public class Schedule
    {
        [Key]
        public Guid ScheduleId { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }

        public Guid BusinessId { get; set; }
        [ForeignKey("BusinessId")]
        public Business Business { get; set; }

        public DateTime ScheduledDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [MaxLength(500)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
