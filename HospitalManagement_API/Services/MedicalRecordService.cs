using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HospitalManagement_API.DataAccess;
using HospitalManagement_API.Models;

namespace HospitalManagement_API.Services
{
    public class MedicalRecordService
    {
        private readonly DB_Connection _db;

        public MedicalRecordService(DB_Connection db)
        {
            _db = db;
        }

        public bool InsertMedicalRecord(MedicalRecordDto medicalRecord)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("InsertMedicalRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patient_id", medicalRecord.PatientId);
                    cmd.Parameters.AddWithValue("@doctor_id", medicalRecord.DoctorId);
                    cmd.Parameters.AddWithValue("@visit_date", medicalRecord.VisitDate);
                    cmd.Parameters.AddWithValue("@diagnosis", medicalRecord.Diagnosis);
                    cmd.Parameters.AddWithValue("@prescribed_treatment", medicalRecord.PrescribedTreatment);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }

        public bool UpdateMedicalRecord(MedicalRecordDto medicalRecord)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("UpdateMedicalRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@record_id", medicalRecord.RecordId);
                    cmd.Parameters.AddWithValue("@diagnosis", medicalRecord.Diagnosis);
                    cmd.Parameters.AddWithValue("@prescribed_treatment", medicalRecord.PrescribedTreatment);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }

        public bool DeleteMedicalRecord(int recordId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("DeleteMedicalRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@record_id", recordId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }

        public List<Dictionary<string, object>> GetMedicalRecordsByPatient(int patientId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetMedicalRecordsByPatient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    DataTable medicalRecords = new DataTable();

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(medicalRecords);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return ConvertDataTableToList(medicalRecords);
                }
            }
        }

        public List<Dictionary<string, object>> GetAllMedicalRecords()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetAllMedicalRecords", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    DataTable medicalRecords = new DataTable();

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(medicalRecords);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return ConvertDataTableToList(medicalRecords);
                }
            }
        }

        public List<Dictionary<string, object>> GetMedicalRecordById(int recordId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetMedicalRecordById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@record_id", recordId);
                    DataTable dt = new DataTable();

                    try
                    {
                        conn.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return ConvertDataTableToList(dt);
                }
            }
        }

        private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
        {
            var rows = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                var rowDict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    rowDict.Add(col.ColumnName, row[col]);
                }
                rows.Add(rowDict);
            }

            return rows;
        }
    }
}
