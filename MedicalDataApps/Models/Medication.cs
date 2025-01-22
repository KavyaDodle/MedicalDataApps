namespace MedicalDataApps.Models
{
    public class Medication
    {
        public required string MedicationID { get; set; }   // Primary Key
        public required string PatientID { get; set; }      // Foreign Key
        public required string DoctorID { get; set; }       // Foreign Key
        public required string MedicationName { get; set; }
        public required DateTime PrescribedDate { get; set; }
        public required string Dosage { get; set; }
        public required string Frequency { get; set; }

        // Navigation Property
        public required Patient Patient { get; set; }
        public required Doctor Doctor { get; set; }
    }
}
