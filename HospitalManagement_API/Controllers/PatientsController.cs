//using HospitalManagement_API.Services;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;

//namespace HospitalManagement_API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PatientsController : ControllerBase
//    {
//        private readonly MainService _mainService;

//        public PatientsController(MainService mainService)
//        {
//            _mainService = mainService;
//        }

//        //[HttpPost]
//        //public IActionResult AddPatient([FromBody] HospitalManagement_API.Models.PatientDto patient)
//        //{
//        //    var result = _mainService.InsertPatient(patient.FirstName, patient.LastName, patient.DateOfBirth, patient.Gender, patient.PhoneNumber, patient.Email, patient.Address, patient.EmergencyContact, patient.BloodGroup);
//        //    return result ? Ok("Patient added successfully") : BadRequest("Failed to add patient");
//        //}

//        //[HttpGet]
//        //public IActionResult GetAllPatients()
//        //{
//        //    DataTable patients = _mainService.GetAllPatients();
//        //    return Ok(patients);
//        //}

//        //[HttpGet("{id}")]
//        //public IActionResult GetPatientByID(int id)
//        //{
//        //    DataTable patient = _mainService.GetPatientByID(id);
//        //    return Ok(patient);
//        //}

//        //[HttpGet("search")]
//        //public IActionResult SearchPatients([FromQuery] string searchTerm)
//        //{
//        //    DataTable results = _mainService.SearchPatients(searchTerm);
//        //    return Ok(results);
//        //}

//        //[HttpPut("{id}")]
//        //public IActionResult UpdatePatient(int id, [FromBody] HospitalManagement_API.Models.PatientDto patient)
//        //{
//        //    var result = _mainService.UpdatePatient(id, patient.FirstName, patient.LastName, patient.DateOfBirth, patient.Gender, patient.PhoneNumber, patient.Email, patient.Address, patient.EmergencyContact, patient.BloodGroup);
//        //    return result ? Ok("Patient updated successfully") : BadRequest("Failed to update patient");
//        //}

//        [HttpGet("GetAllPatients")]
//        public IActionResult GetAllPatients()
//        {
//            try
//            {
//                var patients = _mainService.GetAllPatients();
//                return Ok(patients);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpGet("GetPatientByID/{patientId}")]
//        public IActionResult GetPatientByID(int patientId)
//        {
//            try
//            {
//                var patient = _mainService.GetPatientByID(patientId);
//                if (patient == null || patient.Count == 0)
//                {
//                    return NotFound("Patient not found");
//                }
//                return Ok(patient);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//        [HttpPost("InsertPatient")]
//        public IActionResult InsertPatient([FromBody] HospitalManagement_API.Models.PatientDto patient)
//        {
//            try
//            {
//                var result = _mainService.InsertPatient(
//                    patient.FirstName, patient.LastName, patient.DateOfBirth, patient.Gender,
//                    patient.PhoneNumber, patient.Email, patient.Address, patient.EmergencyContact,
//                    patient.BloodGroup);

//                if (result)
//                {
//                    return Ok("Patient inserted successfully.");
//                }
//                return StatusCode(500, "Failed to insert patient");
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, $"Internal server error: {ex.Message}");
//            }
//        }

//    }
//}


/////////////////////






//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using HospitalManagement_API.DataAccess;
//using HospitalManagement_API.Models;

//namespace HospitalManagement_API.Services
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class PatientService : ControllerBase
//    {
//        private readonly DB_Connection _db;

//        public PatientService(DB_Connection db)
//        {
//            _db = db;
//        }

//        [HttpPost("InsertPatient")]
//        public ActionResult<bool> InsertPatient([FromBody] PatientDto patient)
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                conn.Open();
//                using (SqlCommand cmd = new SqlCommand("InsertPatient", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@first_name", patient.FirstName);
//                    cmd.Parameters.AddWithValue("@last_name", patient.LastName);
//                    cmd.Parameters.AddWithValue("@date_of_birth", patient.DateOfBirth);
//                    cmd.Parameters.AddWithValue("@gender", patient.Gender);
//                    cmd.Parameters.AddWithValue("@phone_number", patient.PhoneNumber);
//                    cmd.Parameters.AddWithValue("@email", patient.Email);
//                    cmd.Parameters.AddWithValue("@address", patient.Address);
//                    cmd.Parameters.AddWithValue("@emergency_contact", patient.EmergencyContact);
//                    cmd.Parameters.AddWithValue("@blood_group", patient.BloodGroup);

//                    cmd.ExecuteNonQuery();
//                    return Ok(true);
//                }
//            }
//        }

//        [HttpPut("UpdatePatient/{patientId}")]
//        public ActionResult<bool> UpdatePatient(int patientId, [FromBody] PatientDto patient)
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                try
//                {
//                    conn.Open();
//                    using (SqlCommand cmd = new SqlCommand("UpdatePatient", conn))
//                    {
//                        cmd.CommandType = CommandType.StoredProcedure;
//                        cmd.Parameters.AddWithValue("@patient_id", patientId);
//                        cmd.Parameters.AddWithValue("@first_name", patient.FirstName);
//                        cmd.Parameters.AddWithValue("@last_name", patient.LastName);
//                        cmd.Parameters.AddWithValue("@date_of_birth", patient.DateOfBirth);
//                        cmd.Parameters.AddWithValue("@gender", patient.Gender);
//                        cmd.Parameters.AddWithValue("@phone_number", patient.PhoneNumber);
//                        cmd.Parameters.AddWithValue("@email", patient.Email);
//                        cmd.Parameters.AddWithValue("@address", patient.Address);
//                        cmd.Parameters.AddWithValue("@emergency_contact", patient.EmergencyContact);
//                        cmd.Parameters.AddWithValue("@blood_group", patient.BloodGroup);

//                        cmd.ExecuteNonQuery();
//                        return Ok(true);
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest(ex.Message);
//                }
//            }
//        }

//        [HttpGet("GetAllPatients")]
//        public ActionResult<List<PatientDto>> GetAllPatients()
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                using (SqlCommand cmd = new SqlCommand("GetAllPatients", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    conn.Open();
//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        var patients = new List<PatientDto>();
//                        while (reader.Read())
//                        {
//                            patients.Add(new PatientDto
//                            {
//                                PatientId = Convert.ToInt32(reader["patient_id"]),
//                                FirstName = reader["first_name"].ToString(),
//                                LastName = reader["last_name"].ToString(),
//                                DateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
//                                Gender = reader["gender"].ToString(),
//                                PhoneNumber = reader["phone_number"].ToString(),
//                                Email = reader["email"].ToString(),
//                                Address = reader["address"].ToString(),
//                                EmergencyContact = reader["emergency_contact"].ToString(),
//                                BloodGroup = reader["blood_group"].ToString()
//                            });
//                        }
//                        return Ok(patients);
//                    }
//                }
//            }
//        }

//        [HttpGet("GetPatientByID/{patientId}")]
//        public ActionResult<PatientDto> GetPatientByID(int patientId)
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                using (SqlCommand cmd = new SqlCommand("GetPatientByID", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@patient_id", patientId);
//                    conn.Open();
//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        if (reader.Read())
//                        {
//                            var patient = new PatientDto
//                            {
//                                PatientId = Convert.ToInt32(reader["patient_id"]),
//                                FirstName = reader["first_name"].ToString(),
//                                LastName = reader["last_name"].ToString(),
//                                DateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
//                                Gender = reader["gender"].ToString(),
//                                PhoneNumber = reader["phone_number"].ToString(),
//                                Email = reader["email"].ToString(),
//                                Address = reader["address"].ToString(),
//                                EmergencyContact = reader["emergency_contact"].ToString(),
//                                BloodGroup = reader["blood_group"].ToString()
//                            };
//                            return Ok(patient);
//                        }
//                        return NotFound();
//                    }
//                }
//            }
//        }

//        [HttpGet("SearchPatients/{searchTerm}")]
//        public ActionResult<List<PatientDto>> SearchPatients(string searchTerm)
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                using (SqlCommand cmd = new SqlCommand("SearchPatients", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
//                    conn.Open();
//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        var patients = new List<PatientDto>();
//                        while (reader.Read())
//                        {
//                            patients.Add(new PatientDto
//                            {
//                                PatientId = Convert.ToInt32(reader["patient_id"]),
//                                FirstName = reader["first_name"].ToString(),
//                                LastName = reader["last_name"].ToString(),
//                                DateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
//                                Gender = reader["gender"].ToString(),
//                                PhoneNumber = reader["phone_number"].ToString(),
//                                Email = reader["email"].ToString(),
//                                Address = reader["address"].ToString(),
//                                EmergencyContact = reader["emergency_contact"].ToString(),
//                                BloodGroup = reader["blood_group"].ToString()
//                            });
//                        }
//                        return Ok(patients);
//                    }
//                }
//            }
//        }
//    }
//}

using HospitalManagement_API.Models;
using HospitalManagement_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HospitalManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly MainService _patientService;

        public PatientController(MainService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost("InsertPatient")]
        public ActionResult<bool> InsertPatient([FromBody] PatientDto patient)
        {
            try
            {
                var result = _patientService.InsertPatient(patient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdatePatient/{patientId}")]
        public ActionResult<bool> UpdatePatient(int patientId, [FromBody] PatientDto patient)
        {
            try
            {
                var result = _patientService.UpdatePatient(patientId, patient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllPatients")]
        public ActionResult<List<PatientDto>> GetAllPatients()
        {
            try
            {
                var patients = _patientService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPatientByID/{patientId}")]
        public ActionResult<PatientDto> GetPatientByID(int patientId)
        {
            try
            {
                var patient = _patientService.GetPatientByID(patientId);
                if (patient == null)
                {
                    return NotFound();
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchPatients/{searchTerm}")]
        public ActionResult<List<PatientDto>> SearchPatients(string searchTerm)
        {
            try
            {
                var patients = _patientService.SearchPatients(searchTerm);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
