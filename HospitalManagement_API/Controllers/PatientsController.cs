using HospitalManagement_API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HospitalManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly MainService _mainService;

        public PatientsController(MainService mainService)
        {
            _mainService = mainService;
        }

        [HttpPost]
        public IActionResult AddPatient([FromBody] HospitalManagement_API.Models.PatientDto patient)
        {
            var result = _mainService.InsertPatient(patient.FirstName, patient.LastName, patient.DateOfBirth, patient.Gender, patient.PhoneNumber, patient.Email, patient.Address, patient.EmergencyContact, patient.BloodGroup);
            return result ? Ok("Patient added successfully") : BadRequest("Failed to add patient");
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            DataTable patients = _mainService.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientByID(int id)
        {
            DataTable patient = _mainService.GetPatientByID(id);
            return Ok(patient);
        }

        [HttpGet("search")]
        public IActionResult SearchPatients([FromQuery] string searchTerm)
        {
            DataTable results = _mainService.SearchPatients(searchTerm);
            return Ok(results);
        }

    }
}
