using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HospitalManagement_API.Models;
using HospitalManagement_API.Services;

namespace HospitalManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("InsertAppointment")]
        public ActionResult<bool> InsertAppointment([FromBody] AppointmentDto appointment)
        {
            if (appointment == null) return BadRequest("Appointment data is required.");

            try
            {
                var result = _appointmentService.InsertAppointment(appointment);
                return result ? Ok(result) : StatusCode(500, "Failed to insert appointment.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetPatientAppointments/{patientId}")]
        public ActionResult<List<Dictionary<string, object>>> GetPatientAppointments(int patientId)
        {
            try
            {
                var appointments = _appointmentService.GetPatientAppointments(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateAppointmentStatus/{appointmentId}")]
        public ActionResult<bool> UpdateAppointmentStatus(int appointmentId, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status)) return BadRequest("Status is required.");

            try
            {
                var result = _appointmentService.UpdateAppointmentStatus(appointmentId, status);
                return result ? Ok(result) : StatusCode(500, "Failed to update appointment status.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAppointmentDetailsById/{appointmentId}")]
        public ActionResult<Dictionary<string, object>> GetAppointmentDetailsById(int appointmentId)
        {
            try
            {
                var appointmentDetails = _appointmentService.GetAppointmentDetailsById(appointmentId);
                if (appointmentDetails == null) return NotFound("Appointment not found.");
                return Ok(appointmentDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
