﻿using AppointDoc.Application.Interfaces;
using AppointDoc.Domain.DbModels;
using AppointDoc.Domain.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        [Route("CreateAppointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            if (appointmentDto.AppointmentDateTime <= DateTime.UtcNow)
                return BadRequest("Appointment date must be in the future.");

            Appointment appointment = new Appointment()
            {
                PatientName = appointmentDto.PatientName,
                PatientContactInformation = appointmentDto.PatientContactInformation,
                AppointmentDateTime = appointmentDto.AppointmentDateTime,
                DoctorId = appointmentDto.DoctorId,
                CreatedAt = DateTime.Now,
                CreatedBy = "Admin",
            };
            bool res = await _appointmentService.AddAsync(appointment);
            if (!res)
            {
                return StatusCode(500, "An error occurred while creating the appointment.");
            }
            return Ok("Appointment created successfully.");
        }

        [HttpGet]
        [Route("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.FindByAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            return Ok(appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment([FromBody] AppointmentDto appointmentDto)
        {
            Appointment appointment = new Appointment()
            {
                PatientName = appointmentDto.PatientName,
                PatientContactInformation = appointmentDto.PatientContactInformation,
                AppointmentDateTime = appointmentDto.AppointmentDateTime,
                DoctorId = appointmentDto.DoctorId,
                UpdatedAt = DateTime.Now,
                UpdatedBy = "AdminX",
            };
            bool updated = await _appointmentService.UpdateAsync(appointment);
            if (!updated)
                return NotFound("Appointment not found or update failed.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var deleted = await _appointmentService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Appointment not found or delete failed.");

            return NoContent();
        }
    }
}