using System.ComponentModel.DataAnnotations;

namespace Appointment.Domain.Model
{
    public class AppointmentEntity : BaseEntity
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        [Required]
        public string? BookedBy { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
