using System.Numerics;

namespace MedicalDataApps.Models
{
    public class Appointment
    {
        public required string AppointmentID { get; set; }  // Primary Key
        public required string PatientID { get; set; }      // Foreign Key
        public required string DoctorID { get; set; }       // Foreign Key
        public required DateTime AppointmentDate { get; set; }
        public required string Purpose { get; set; }

        // Navigation Properties
        public required Patient Patient { get; set; }
        public required Doctor Doctor { get; set; }
    }
}
