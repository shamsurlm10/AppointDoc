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
        public string PatientName { get; set; } = string.Empty;
        public string PatientContactInformation { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; } = DateTime.Now;
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
        public string? CreatedBy { get; set;}
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
