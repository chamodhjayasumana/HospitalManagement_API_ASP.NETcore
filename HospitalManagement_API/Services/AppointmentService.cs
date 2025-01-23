using System;
using System.Data;
using System.Data.SqlClient;
using HospitalManagement_API.DataAccess;
using HospitalManagement_API.Models;

namespace HospitalManagement_API.Services
{
    public class AppointmentService
    {
        private readonly DB_Connection _db;

        public AppointmentService(DB_Connection db)
        {
            _db = db;
        }

        public bool InsertAppointment(AppointmentDto appointment)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("InsertAppointment", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patient_id", appointment.PatientId);
                    cmd.Parameters.AddWithValue("@doctor_id", appointment.DoctorId);
                    cmd.Parameters.AddWithValue("@appointment_date", appointment.AppointmentDateTime);
                    cmd.Parameters.AddWithValue("@reason_for_visit", appointment.ReasonForVisit);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting appointment: {ex.Message}");
                return false;
            }
        }

        public DataTable GetPatientAppointments(int patientId)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("GetPatientAppointments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@patient_id", patientId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching patient appointments: {ex.Message}");
                return new DataTable();
            }
        }

        public bool UpdateAppointmentStatus(int appointmentId, string status)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("UpdateAppointmentStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@appointment_id", appointmentId);
                    cmd.Parameters.AddWithValue("@status", status);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating appointment status: {ex.Message}");
                return false;
            }
        }

        public DataTable GetAppointmentDetailsById(int appointmentId)
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = new SqlCommand("GetAppointmentDetailsById", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@appointment_id", appointmentId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointment details: {ex.Message}");
                return new DataTable();
            }
        }
    }
}
