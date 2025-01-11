using System.ComponentModel.DataAnnotations;

namespace AppointDoc.Domain.DbModels
{
    public sealed class Doctor
    {
        public Guid DoctorId { get; set; }
        [Required]
        [MaxLength(100)]
        public string DoctorName { get; set; } = string.Empty;
    }
}
