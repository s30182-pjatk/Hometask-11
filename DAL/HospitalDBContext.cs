using Microsoft.EntityFrameworkCore;
using Tutorial11.Models;

namespace Tutorial11.DAL;

public class HospitalDBContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

    protected HospitalDBContext()
    {
    }

    public HospitalDBContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Patient>()
            .Property(p => p.IdPatient)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<Prescription>()
            .Property(e => e.Date)
            .HasColumnType("date");
        
        modelBuilder.Entity<Prescription>()
            .Property(e => e.DueDate)
            .HasColumnType("date");
        
        // Sample Patients
        modelBuilder.Entity<Patient>().HasData(
            new Patient { IdPatient = 1, FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 5, 1) },
            new Patient { IdPatient = 2, FirstName = "Jane", LastName = "Smith", BirthDate = new DateTime(1985, 10, 15) }
        );

        // Sample Doctors
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { IdDoctor = 1, FirstName = "Gregory", LastName = "House", Email = "house@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "Meredith", LastName = "Grey", Email = "grey@example.com" }
        );

        // Sample Medicaments
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { IdMedicament = 1, Name = "Aspirin", Description = "Pain relief", Type = "Tablet" },
            new Medicament { IdMedicament = 2, Name = "Penicillin", Description = "Antibiotic", Type = "Injection" }
        );

        // Sample Prescriptions
        modelBuilder.Entity<Prescription>().HasData(
            new Prescription { IdPrescription = 1, Date = new DateTime(2023, 1, 10), DueDate = new DateTime(2023, 2, 10), PatientId = 1, DoctorId = 1 },
            new Prescription { IdPrescription = 2, Date = new DateTime(2023, 1, 12), DueDate = new DateTime(2023, 2, 12), PatientId = 2, DoctorId = 2 }
        );

        // Sample Prescription_Medicaments
        modelBuilder.Entity<Prescription_Medicament>().HasData(
            new Prescription_Medicament { MedicamentId = 1, PrescriptionId = 1, Dose = 2, Details = "Take twice daily" },
            new Prescription_Medicament { MedicamentId = 2, PrescriptionId = 1, Dose = 1, Details = "Once in the morning" },
            new Prescription_Medicament { MedicamentId = 1, PrescriptionId = 2, Dose = 1, Details = "Before sleep" }
        );
    }
}