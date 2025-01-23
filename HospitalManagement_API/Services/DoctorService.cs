//using System;
//using System.Data;
//using System.Data.SqlClient;
//using HospitalManagement_API.DataAccess;
//using HospitalManagement_API.Models;

//namespace HospitalManagement_API.Services
//{
//    public class DoctorService
//    {
//        private readonly DB_Connection _db;

//        public DoctorService(DB_Connection db)
//        {
//            _db = db;
//        }

//        public DataTable GetAllDoctors()
//        {
//            using (SqlConnection conn = _db.GetConnection())
//            using (SqlDataAdapter da = new SqlDataAdapter("SELECT doctor_id, CONCAT(first_name, ' ', last_name) AS full_name FROM Doctors", conn))
//            {
//                DataTable dt = new DataTable();
//                da.Fill(dt);
//                return dt;
//            }
//        }

//        public bool InsertDoctor(string firstName, string lastName, string specialization, string phoneNumber, string email, int departmentId)
//        {
//            try
//            {
//                using (SqlConnection conn = _db.GetConnection())
//                using (SqlCommand cmd = new SqlCommand("InsertDoctor", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@FirstName", firstName);
//                    cmd.Parameters.AddWithValue("@LastName", lastName);
//                    cmd.Parameters.AddWithValue("@Specialization", specialization);
//                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
//                    cmd.Parameters.AddWithValue("@Email", email);
//                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

//                    conn.Open();
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error inserting doctor: {ex.Message}");
//                return false;
//            }
//        }

//        public bool UpdateDoctor(int doctorId, string firstName, string lastName, string specialization, string phoneNumber, string email, int departmentId)
//        {
//            try
//            {
//                using (SqlConnection conn = _db.GetConnection())
//                using (SqlCommand cmd = new SqlCommand("UpdateDoctor", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@doctor_id", doctorId);
//                    cmd.Parameters.AddWithValue("@first_name", firstName);
//                    cmd.Parameters.AddWithValue("@last_name", lastName);
//                    cmd.Parameters.AddWithValue("@specialization", specialization);
//                    cmd.Parameters.AddWithValue("@phone_number", phoneNumber);
//                    cmd.Parameters.AddWithValue("@email", email);
//                    cmd.Parameters.AddWithValue("@department_id", departmentId);

//                    conn.Open();
//                    return cmd.ExecuteNonQuery() > 0;
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error updating doctor: {ex.Message}");
//                return false;
//            }
//        }

//        public DataTable GetDoctorByID(int doctorId)
//        {
//            try
//            {
//                using (SqlConnection conn = _db.GetConnection())
//                using (SqlCommand cmd = new SqlCommand("GetDoctorByID", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@doctor_id", doctorId);

//                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
//                    {
//                        DataTable dtDoctor = new DataTable();
//                        adapter.Fill(dtDoctor);
//                        return dtDoctor;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
//                return new DataTable();
//            }
//        }

//        public DataTable GetDoctorsByDepartment(int departmentId)
//        {
//            try
//            {
//                using (SqlConnection conn = _db.GetConnection())
//                using (SqlCommand cmd = new SqlCommand("GetDoctorsByDepartment", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@DepartmentID", departmentId);

//                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
//                    {
//                        DataTable dt = new DataTable();
//                        adapter.Fill(dt);
//                        return dt;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error retrieving doctors by department: {ex.Message}");
//                return new DataTable();
//            }
//        }
//    }
//}

using HospitalManagement_API.DataAccess;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class DoctorService
{
    private readonly DB_Connection _db;

    public DoctorService(DB_Connection db)
    {
        _db = db;
    }

    // Method to get all doctors in a serializable format
    public List<Dictionary<string, object>> GetAllDoctors()
    {
        try
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "SELECT doctor_id, CONCAT(first_name, ' ', last_name) AS full_name FROM Doctors";
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return ConvertDataTableToList(dt);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving all doctors: {ex.Message}");
            return new List<Dictionary<string, object>>();
        }
    }

    // Method to get a doctor by ID
    public List<Dictionary<string, object>> GetDoctorById(int doctorId)
    {
        try
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                string query = "SELECT * FROM Doctors WHERE doctor_id = @DoctorId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DoctorId", doctorId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        return ConvertDataTableToList(dt);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
            return new List<Dictionary<string, object>>();
        }
    }

    // Helper method to convert DataTable to a list of dictionaries
    private List<Dictionary<string, object>> ConvertDataTableToList(DataTable dt)
    {
        var result = new List<Dictionary<string, object>>();

        foreach (DataRow row in dt.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn column in dt.Columns)
            {
                dict[column.ColumnName] = row[column];
            }
            result.Add(dict);
        }

        return result;
    }
}



