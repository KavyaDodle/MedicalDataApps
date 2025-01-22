using MedicalDataApps.Models;
using Microsoft.EntityFrameworkCore;

public class MedicalDbContext : DbContext
{
    public MedicalDbContext(DbContextOptions<MedicalDbContext> options) : base(options) { }

    public DbSet<Patient> Patient { get; set; }
    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Medication> Medication { get; set; }
}
