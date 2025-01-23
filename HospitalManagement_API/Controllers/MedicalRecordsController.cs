using HospitalManagement_API.Models;
using HospitalManagement_API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HospitalManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly MedicalRecordService _medicalRecordService;

        public MedicalRecordsController(MedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        [HttpPost("InsertMedicalRecord")]
        public ActionResult<bool> InsertMedicalRecord([FromBody] MedicalRecordDto medicalRecord)
        {
            if (medicalRecord == null) return BadRequest("Medical record data is required.");

            try
            {
                var result = _medicalRecordService.InsertMedicalRecord(medicalRecord);
                return result ? Ok(result) : StatusCode(500, "Failed to insert medical record.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateMedicalRecord")]
        public ActionResult<bool> UpdateMedicalRecord([FromBody] MedicalRecordDto medicalRecord)
        {
            try
            {
                var result = _medicalRecordService.UpdateMedicalRecord(medicalRecord);
                return result ? Ok(result) : StatusCode(500, "Failed to update medical record.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteMedicalRecord/{recordId}")]
        public ActionResult<bool> DeleteMedicalRecord(int recordId)
        {
            try
            {
                var result = _medicalRecordService.DeleteMedicalRecord(recordId);
                return result ? Ok(result) : StatusCode(500, "Failed to delete medical record.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetMedicalRecordsByPatient/{patientId}")]
        public ActionResult<List<Dictionary<string, object>>> GetMedicalRecordsByPatient(int patientId)
        {
            try
            {
                var records = _medicalRecordService.GetMedicalRecordsByPatient(patientId);
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAllMedicalRecords")]
        public ActionResult<List<Dictionary<string, object>>> GetAllMedicalRecords()
        {
            try
            {
                var records = _medicalRecordService.GetAllMedicalRecords();
                return Ok(records);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetMedicalRecordById/{recordId}")]
        public ActionResult<List<Dictionary<string, object>>> GetMedicalRecordById(int recordId)
        {
            try
            {
                var record = _medicalRecordService.GetMedicalRecordById(recordId);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
