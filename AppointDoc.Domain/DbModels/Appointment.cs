using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Domain.DbModels
{
    public sealed class Appointment
    {
        public Guid AppointmentId { get; set; }
        [Required]
        public string PatientName { get; set; } = string.Empty;
        public required string PatientContactInformation { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; } // Must be in the future
        public int DoctorId { get; set; } // Foreign Key
        public required Doctor Doctor { get; set; } // Navigation Property
    }
}
