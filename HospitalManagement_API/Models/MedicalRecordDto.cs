namespace HospitalManagement_API.Models
{
    public class MedicalRecordDto
    {
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Diagnosis { get; set; }
        public string PrescribedTreatment { get; set; }
    }
}
