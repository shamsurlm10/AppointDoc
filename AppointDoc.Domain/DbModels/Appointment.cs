using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AppointDoc.Domain.DbModels
{
    public sealed class Appointment
    {
        public Guid AppointmentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string PatientName { get; set; } = string.Empty;
        [Required]
        [MaxLength(15)]
        public string PatientContactInformation { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; } = DateTime.Now;
        [Required]
        public Guid DoctorId { get; set; }
        [JsonIgnore]
        public Doctor? Doctor { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
        public string? CreatedBy { get; set;}
        public DateTime? UpdatedAt { get; set; } = null;
        public string? UpdatedBy { get; set; }
    }
}
