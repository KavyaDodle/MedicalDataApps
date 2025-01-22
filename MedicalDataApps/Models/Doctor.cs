namespace MedicalDataApps.Models
{
    public class Doctor
    {
        public required string DoctorID { get; set; }  // Primary Key
        public required string Name { get; set; }
        public required string Specialty { get; set; }
        public required string ContactInfo { get; set; }
    }
}
