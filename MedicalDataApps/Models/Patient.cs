namespace MedicalDataApps.Models
{
    public class Patient
    {
        public required string PatientID { get; set; } //Primary Key
        public required string Name { get; set; }
        public required int Age { get; set; }
        public required string Gender { get; set; }
        public required string ContactInfo { get; set; }
    }
}
