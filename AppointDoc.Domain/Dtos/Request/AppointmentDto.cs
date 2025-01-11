using AppointDoc.Domain.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Domain.Dtos.Request
{
    public class AppointmentDto
    {
        public string PatientName { get; set; } = string.Empty;
        public string PatientContactInformation { get; set; } = string.Empty;
        public DateTime AppointmentDateTime { get; set; }
        public Guid DoctorId { get; set; }
    }
}
