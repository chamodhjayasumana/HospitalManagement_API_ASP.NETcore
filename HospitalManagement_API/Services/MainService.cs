
using System;
using System.Data;
using System.Data.SqlClient;
using HospitalManagement_API.DataAccess;
using HospitalManagement_API.Models;

namespace HospitalManagement_API.Services
{
    public class MainService
    {
        private readonly DB_Connection _db;

        public MainService(DB_Connection db)
        {
            _db = db;
        }
        public bool InsertPatient(PatientDto patient)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertPatient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@first_name", patient.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", patient.LastName);
                    cmd.Parameters.AddWithValue("@date_of_birth", patient.DateOfBirth);
                    cmd.Parameters.AddWithValue("@gender", patient.Gender);
                    cmd.Parameters.AddWithValue("@phone_number", patient.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", patient.Email);
                    cmd.Parameters.AddWithValue("@address", patient.Address);
                    cmd.Parameters.AddWithValue("@emergency_contact", patient.EmergencyContact);
                    cmd.Parameters.AddWithValue("@blood_group", patient.BloodGroup);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool UpdatePatient(int patientId, PatientDto patient)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdatePatient", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@patient_id", patientId);
                        cmd.Parameters.AddWithValue("@first_name", patient.FirstName);
                        cmd.Parameters.AddWithValue("@last_name", patient.LastName);
                        cmd.Parameters.AddWithValue("@date_of_birth", patient.DateOfBirth);
                        cmd.Parameters.AddWithValue("@gender", patient.Gender);
                        cmd.Parameters.AddWithValue("@phone_number", patient.PhoneNumber);
                        cmd.Parameters.AddWithValue("@email", patient.Email);
                        cmd.Parameters.AddWithValue("@address", patient.Address);
                        cmd.Parameters.AddWithValue("@emergency_contact", patient.EmergencyContact);
                        cmd.Parameters.AddWithValue("@blood_group", patient.BloodGroup);

                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<PatientDto> GetAllPatients()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetAllPatients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var patients = new List<PatientDto>();
                        while (reader.Read())
                        {
                            patients.Add(new PatientDto
                            {
                                PatientId = Convert.ToInt32(reader["patient_id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
                                Gender = reader["gender"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString(),
                                Email = reader["email"].ToString(),
                                Address = reader["address"].ToString(),
                                EmergencyContact = reader["emergency_contact"].ToString(),
                                BloodGroup = reader["blood_group"].ToString()
                            });
                        }
                        return patients;
                    }
                }
            }
        }

        public PatientDto GetPatientByID(int patientId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("GetPatientByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patient_id", patientId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PatientDto
                            {
                                PatientId = Convert.ToInt32(reader["patient_id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
                                Gender = reader["gender"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString(),
                                Email = reader["email"].ToString(),
                                Address = reader["address"].ToString(),
                                EmergencyContact = reader["emergency_contact"].ToString(),
                                BloodGroup = reader["blood_group"].ToString()
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public List<PatientDto> SearchPatients(string searchTerm)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SearchPatients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var patients = new List<PatientDto>();
                        while (reader.Read())
                        {
                            patients.Add(new PatientDto
                            {
                                PatientId = Convert.ToInt32(reader["patient_id"]),
                                FirstName = reader["first_name"].ToString(),
                                LastName = reader["last_name"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["date_of_birth"]),
                                Gender = reader["gender"].ToString(),
                                PhoneNumber = reader["phone_number"].ToString(),
                                Email = reader["email"].ToString(),
                                Address = reader["address"].ToString(),
                                EmergencyContact = reader["emergency_contact"].ToString(),
                                BloodGroup = reader["blood_group"].ToString()
                            });
                        }
                        return patients;
                    }
                }
            }
        }

    }
}
