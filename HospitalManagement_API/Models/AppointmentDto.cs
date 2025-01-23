namespace HospitalManagement_API.Models
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string ReasonForVisit { get; set; }
        public int AppointmentId { get; set; }
        public string Status { get; set; }
    }
}
