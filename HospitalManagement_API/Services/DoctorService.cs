using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HospitalManagement_API.DataAccess;
using HospitalManagement_API.Models;

namespace HospitalManagement_API.Services
{
    public class DoctorService
    {
        private readonly DB_Connection _db;

        public DoctorService(DB_Connection db)
        {
            _db = db;
        }

        public List<DoctorDto> GetAllDoctors()
        {
            var doctors = new List<DoctorDto>();

            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("SELECT doctor_id, first_name, last_name, specialization, phone_number, email, department_id FROM Doctors", conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new DoctorDto
                            {
                                DoctorId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Specialization = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctors: {ex.Message}");
            }

            return doctors;
        }

        public bool InsertDoctor(DoctorDto doctor)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("InsertDoctor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", doctor.LastName);
                    cmd.Parameters.AddWithValue("@Specialization", doctor.Specialization);
                    cmd.Parameters.AddWithValue("@PhoneNumber", doctor.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", doctor.Email);
                    cmd.Parameters.AddWithValue("@DepartmentID", doctor.DepartmentId);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting doctor: {ex.Message}");
                return false;
            }
        }

        public bool UpdateDoctor(DoctorDto doctor)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("UpdateDoctor", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doctor_id", doctor.DoctorId);
                    cmd.Parameters.AddWithValue("@first_name", doctor.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", doctor.LastName);
                    cmd.Parameters.AddWithValue("@specialization", doctor.Specialization);
                    cmd.Parameters.AddWithValue("@phone_number", doctor.PhoneNumber);
                    cmd.Parameters.AddWithValue("@email", doctor.Email);
                    cmd.Parameters.AddWithValue("@department_id", doctor.DepartmentId);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating doctor: {ex.Message}");
                return false;
            }
        }

        public DoctorDto GetDoctorByID(int doctorId)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("GetDoctorByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@doctor_id", doctorId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DoctorDto
                            {
                                DoctorId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Specialization = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.GetInt32(6)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
            }

            return null;
        }

        public List<DoctorDto> GetDoctorsByDepartment(int departmentId)
        {
            var doctors = new List<DoctorDto>();

            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("GetDoctorsByDepartment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new DoctorDto
                            {
                                DoctorId = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                Specialization = reader.GetString(3),
                                PhoneNumber = reader.GetString(4),
                                Email = reader.GetString(5),
                                DepartmentId = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctors by department: {ex.Message}");
            }

            return doctors;
        }
    }
}



//////////////////////

//using HospitalManagement_API.DataAccess;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;

//public class DoctorService
//{
//    private readonly DB_Connection _db;

//    public DoctorService(DB_Connection db)
//    {
//        _db = db;
//    }

//    // Method to get all doctors in a serializable format
//    public List<Dictionary<string, object>> GetAllDoctors()
//    {
//        try
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                string query = "SELECT doctor_id, CONCAT(first_name, ' ', last_name) AS full_name FROM Doctors";
//                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
//                {
//                    DataTable dt = new DataTable();
//                    da.Fill(dt);

//                    return ConvertDataTableToList(dt);
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error retrieving all doctors: {ex.Message}");
//            return new List<Dictionary<string, object>>();
//        }
//    }

//    // Method to get a doctor by ID
//    public List<Dictionary<string, object>> GetDoctorById(int doctorId)
//    {
//        try
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            {
//                string query = "SELECT * FROM Doctors WHERE doctor_id = @DoctorId";
//                using (SqlCommand cmd = new SqlCommand(query, conn))
//                {
//                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);

//                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
//                    {
//                        DataTable dt = new DataTable();
//                        da.Fill(dt);

//                        return ConvertDataTableToList(dt);
//                    }
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
//            return new List<Dictionary<string, object>>();
//        }
//    }

//    // Helper method to convert DataTable to a list of dictionaries
//    private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
//    {
//        var result = new List<Dictionary<string, object>>();

//        foreach (DataRow row in dt.Rows)
//        {
//            var dict = new Dictionary<string, object>();
//            foreach (DataColumn column in dt.Columns)
//            {
//                dict[column.ColumnName] = row[column];
//            }
//            result.Add(dict);
//        }

//        return result;
//    }
//}



