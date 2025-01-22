using System;

namespace HospitalManagement_API.Models
{
    public class PatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string EmergencyContact { get; set; }
        public string BloodGroup { get; set; }
        public int PatientId { get; internal set; }
    }
}
