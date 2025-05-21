using APBD_zajecia11.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_zajecia11.DAL;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext() {}
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
    {
        new Doctor
        {
            IdDoctor = 1,
            FirstName = "Anna",
            LastName = "Kowalska",
            Email = "anna.kowalska@example.com"
        },
        new Doctor
        {
            IdDoctor = 2,
            FirstName = "Piotr",
            LastName = "Nowak",
            Email = "piotr.nowak@example.com"
        }
    });

    modelBuilder.Entity<Patient>().HasData(new List<Patient>()
    {
        new Patient
        {
            IdPatient = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            BirthDate = new DateOnly(1980, 3, 15)
        },
        new Patient
        {
            IdPatient = 2,
            FirstName = "Maria",
            LastName = "Wiśniewska",
            BirthDate = new DateOnly(1990, 7, 21)
        }
    });

    modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
    {
        new Medicament
        {
            IdMedicament = 1,
            Name = "Ibuprofen",
            Description = "Lek przeciwbólowy i przeciwzapalny",
            Type = "Tabletka"
        },
        new Medicament
        {
            IdMedicament = 2,
            Name = "Amoxicillin",
            Description = "Antybiotyk z grupy penicylin",
            Type = "Kapsułka"
        }
    });

    modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
    {
        new Prescription
        {
            IdPrescription = 1,
            Date = new DateOnly(2025, 5, 20),
            DueDate = new DateOnly(2025, 6, 20),
            IdPatient = 1,
            IdDoctor = 1
        }
    });

    modelBuilder.Entity<Prescription_Medicament>().HasData(new List<Prescription_Medicament>()
    {
        new Prescription_Medicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 200,
            Details = "Przyjmować co 8 godzin"
        },
        new Prescription_Medicament
        {
            IdMedicament = 2,
            IdPrescription = 1,
            Dose = 500,
            Details = "Przyjmować co 12 godzin"
        }
    });
}

}