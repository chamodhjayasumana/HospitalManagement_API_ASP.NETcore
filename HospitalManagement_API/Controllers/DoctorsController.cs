using System;
using System.Data;
using HospitalManagement_API.Services;
using HospitalManagement_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_OPD.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("GetAllDoctors")]
        public ActionResult<DataTable> GetAllDoctors()
        {
            try
            {
                var doctors = _doctorService.GetAllDoctors();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("InsertDoctor")]
        public ActionResult<bool> InsertDoctor([FromBody] DoctorDto doctor)
        {
            if (doctor == null) return BadRequest("Doctor data is required.");
            try
            {
                var result = _doctorService.InsertDoctor(doctor.FirstName, doctor.LastName, doctor.Specialization, doctor.PhoneNumber, doctor.Email, doctor.DepartmentId);
                return result ? Ok(result) : StatusCode(500, "Failed to insert doctor.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateDoctor/{doctorId}")]
        public ActionResult<bool> UpdateDoctor(int doctorId, [FromBody] DoctorDto doctor)
        {
            if (doctor == null) return BadRequest("Doctor data is required.");
            try
            {
                var result = _doctorService.UpdateDoctor(doctorId, doctor.FirstName, doctor.LastName, doctor.Specialization, doctor.PhoneNumber, doctor.Email, doctor.DepartmentId);
                return result ? Ok(result) : StatusCode(500, "Failed to update doctor.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetDoctorByID/{doctorId}")]
        public ActionResult<DataTable> GetDoctorByID(int doctorId)
        {
            try
            {
                var doctor = _doctorService.GetDoctorByID(doctorId);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetDoctorsByDepartment/{departmentId}")]
        public ActionResult<DataTable> GetDoctorsByDepartment(int departmentId)
        {
            try
            {
                var doctors = _doctorService.GetDoctorsByDepartment(departmentId);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
