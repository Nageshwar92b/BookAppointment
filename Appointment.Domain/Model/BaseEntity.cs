using System.ComponentModel.DataAnnotations;

namespace Appointment.Domain.Model
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "System";
        public string UpdatedBy { get; set; } = "System";
    }
}
