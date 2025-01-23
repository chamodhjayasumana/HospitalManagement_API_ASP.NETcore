using System;
using System.Collections.Generic;
using HospitalManagement_API.Models;
using HospitalManagement_API.Services;
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
        public ActionResult<List<DoctorDto>> GetAllDoctors()
        {
            try
            {
                var doctors = _doctorService.GetAllDoctors();
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("InsertDoctor")]
        public ActionResult<bool> InsertDoctor([FromBody] DoctorDto doctor)
        {
            if (doctor == null) return BadRequest("Doctor data is required.");
            try
            {
                var result = _doctorService.InsertDoctor(doctor);
                return result ? Ok(true) : StatusCode(500, "Failed to insert doctor.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateDoctor")]
        public ActionResult<bool> UpdateDoctor([FromBody] DoctorDto doctor)
        {
            if (doctor == null) return BadRequest("Doctor data is required.");
            try
            {
                var result = _doctorService.UpdateDoctor(doctor);
                return result ? Ok(true) : StatusCode(500, "Failed to update doctor.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetDoctorByID/{doctorId}")]
        public ActionResult<DoctorDto> GetDoctorByID(int doctorId)
        {
            try
            {
                var doctor = _doctorService.GetDoctorByID(doctorId);
                return doctor != null ? Ok(doctor) : NotFound("Doctor not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetDoctorsByDepartment/{departmentId}")]
        public ActionResult<List<DoctorDto>> GetDoctorsByDepartment(int departmentId)
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



////////////////////////


//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;

//[Route("api/[controller]")]
//[ApiController]
//public class DoctorsController : ControllerBase
//{
//    private readonly DoctorService _doctorService;

//    public DoctorsController(DoctorService doctorService)
//    {
//        _doctorService = doctorService;
//    }

//    // Endpoint to get all doctors
//    [HttpGet("GetAllDoctors")]
//    public ActionResult<List<Dictionary<string, object>>> GetAllDoctors()
//    {
//        try
//        {
//            var doctors = _doctorService.GetAllDoctors();
//            if (doctors.Count == 0)
//            {
//                return NotFound("No doctors found.");
//            }

//            return Ok(doctors);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, $"Internal server error: {ex.Message}");
//        }
//    }

//    // Endpoint to get a doctor by ID
//    [HttpGet("GetDoctorById/{id}")]
//    public ActionResult<List<Dictionary<string, object>>> GetDoctorById(int id)
//    {
//        try
//        {
//            var doctor = _doctorService.GetDoctorById(id);
//            if (doctor.Count == 0)
//            {
//                return NotFound($"Doctor with ID {id} not found.");
//            }

//            return Ok(doctor);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, $"Internal server error: {ex.Message}");
//        }
//    }
//}
