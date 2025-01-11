namespace AppointDoc.Domain.Dtos.Response
{
    public class AppointmentResponseDto
    {
        public Guid AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string PatientContactInformation { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public Guid DoctorId { get; set; }
    }
}
