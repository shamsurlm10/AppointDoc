namespace AppointDoc.Domain.Dtos.Request
{
    public class AppointmentRequestDto
    {
        public string PatientName { get; set; }
        public string PatientContactInformation { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public Guid DoctorId { get; set; }
    }
}
