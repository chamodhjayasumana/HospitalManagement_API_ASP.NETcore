using System;

namespace HospitalManagement_API.Models
{
    public class DoctorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
    }
}
