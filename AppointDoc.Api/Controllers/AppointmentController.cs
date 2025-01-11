using AppointDoc.Application.Interfaces;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using AppointDoc.Domain.Dtos.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;
namespace AppointDoc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        #region Dependencies

        private readonly IAppointmentService _appointmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentController"/> class.
        /// </summary>
        /// <param name="appointmentService">Service for managing appointments.</param>
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        #endregion

        #region Create Appointment

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="appointmentDto">The appointment details.</param>
        /// <returns>Status of the appointment creation.</returns>
        [Authorize]
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequestDto appointmentDto)
        {
            try
            {
                // Validate that the appointment date is in the future
                if (appointmentDto.AppointmentDateTime <= DateTime.UtcNow)
                    return BadRequest("Appointment date must be in the future.");

                // Check if the provided doctor exists in the system
                bool isValidDoctor = await _appointmentService.IsDoctorExist(appointmentDto.DoctorId);
                if (!isValidDoctor)
                    return BadRequest("Doctor not found.");

                // Add the appointment and check if it was successful
                Appointment appointment = new Appointment
                {
                    PatientName = appointmentDto.PatientName,
                    PatientContactInformation = appointmentDto.PatientContactInformation,
                    AppointmentDateTime = appointmentDto.AppointmentDateTime,
                    DoctorId = appointmentDto.DoctorId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier),
                };

                bool result = await _appointmentService.AddAsync(appointment);
                if (!result)
                    return StatusCode(500, "An error occurred while creating the appointment.");
                
                return Ok("Appointment created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Get All Appointments

        /// <summary>
        /// Retrieves all appointments.
        /// </summary>
        /// <returns>List of all appointments.</returns>
        [Authorize]
        [HttpGet("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            // Get all appointments
            var appointments = await _appointmentService.GetAllAsync();

            // Check for appointments
            if (appointments == null || !appointments.Any())
                return NotFound("No appointments available.");

            var result = appointments.Select(a => new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                PatientName = a.PatientName,
                PatientContactInformation = a.PatientContactInformation,
                AppointmentDateTime = a.AppointmentDateTime,
                DoctorId = a.DoctorId,
            }).ToList();

            return Ok(result);
        }

        #endregion

        #region Get Appointment by ID

        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>Appointment details.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            // Get the appointment if exist
            var appointment = await _appointmentService.FindByAsync(id);

            // Check for appointments
            if (appointment == null)
                return NotFound("Appointment not found.");

            var response = new AppointmentResponseDto
            {
                AppointmentId = appointment.AppointmentId,
                PatientName = appointment.PatientName,
                PatientContactInformation = appointment.PatientContactInformation,
                AppointmentDateTime = appointment.AppointmentDateTime,
                DoctorId = appointment.DoctorId,
            };

            return Ok(response);
        }

        #endregion

        #region Update Appointment

        /// <summary>
        /// Updates an existing appointment.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <param name="appointmentDto">Updated appointment details.</param>
        /// <returns>Status of the update.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] AppointmentRequestDto appointmentDto)
        {
            try
            {
                // Get the appointment if exist
                var appointment = await _appointmentService.FindByAsync(id);

                if (appointment == null)
                    return NotFound("Appointment not found.");

                appointment.PatientName = appointmentDto.PatientName;
                appointment.PatientContactInformation = appointmentDto.PatientContactInformation;
                appointment.AppointmentDateTime = appointmentDto.AppointmentDateTime;
                appointment.DoctorId = appointmentDto.DoctorId;
                appointment.UpdatedAt = DateTime.UtcNow;
                appointment.UpdatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Update the existing appointment entity with new data
                bool updated = await _appointmentService.UpdateAsync(appointment);
                if (!updated)
                    return StatusCode(500, "Appointment update failed.");

                return Ok("Appointment updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        #endregion

        #region Delete Appointment

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The appointment ID.</param>
        /// <returns>Status of the deletion.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            // Get the appointment if exist
            var appointment = await _appointmentService.FindByAsync(id);

            if (appointment == null)
                return NotFound("Appointment not found.");

            // Delete the appointment and check if successful
            bool deleted = await _appointmentService.DeleteAsync(id);
            if (!deleted)
                return StatusCode(500, "Appointment deletion failed.");

            return Ok("Appointment deleted successfully.");
        }

        #endregion
    }
}
