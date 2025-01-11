using AppointDoc.Application.Interfaces;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace AppointDoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [Authorize]
        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequestDto appointmentDto)
        {
            try
            {
                if (appointmentDto.AppointmentDateTime <= DateTime.UtcNow)
                    return BadRequest("Appointment date must be in the future.");
                bool isValidDoctor = await _appointmentService.IsDoctorExist(appointmentDto.DoctorId);
                if (!isValidDoctor)
                    return BadRequest("Doctor not found.");
                Appointment appointment = new Appointment()
                {
                    PatientName = appointmentDto.PatientName,
                    PatientContactInformation = appointmentDto.PatientContactInformation,
                    AppointmentDateTime = appointmentDto.AppointmentDateTime,
                    DoctorId = appointmentDto.DoctorId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier),
                };
                bool res = await _appointmentService.AddAsync(appointment);
                if (!res)
                {
                    return StatusCode(500, "An error occurred while creating the appointment.");
                }
                return Ok("Appointment created successfully.");
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAsync();
            List<AppointmentResponseDto> result = new List<AppointmentResponseDto>();
            result = appointments.Select(appointment => new AppointmentResponseDto
            {
                AppointmentId = appointment.AppointmentId,
                PatientName = appointment.PatientName,
                PatientContactInformation = appointment.PatientContactInformation,
                AppointmentDateTime = appointment.AppointmentDateTime,
                DoctorId = appointment.DoctorId,
            }).ToList();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.FindByAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            return Ok(appointment);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentRequestDto appointmentDto)
        {
            try
            {
                Appointment appointment = new Appointment()
                {
                    PatientName = appointmentDto.PatientName,
                    PatientContactInformation = appointmentDto.PatientContactInformation,
                    AppointmentDateTime = appointmentDto.AppointmentDateTime,
                    DoctorId = appointmentDto.DoctorId,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                bool updated = await _appointmentService.UpdateAsync(appointment);
                if (!updated)
                    return NotFound("Appointment not found or update failed.");

                return Ok("Appointment Successfully Updated!!");
            }
            catch(Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var deleted = await _appointmentService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Appointment not found or delete failed.");

           return Ok("Appointment Successfully Deleted!!");
        }
    }
}
