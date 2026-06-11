using APBD_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD_Template.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> opt) : DbContext(opt)
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<MedicalService> MedicalServices { get; set; }
    public DbSet<AppointmentService> AppointmentServices { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<AppointmentService>()
        .HasKey(e => new { e.AppointmentId, e.ServiceId });

    modelBuilder.Entity<Patient>()
        .HasMany(e => e.Appointments)
        .WithOne(e => e.Patient)
        .HasForeignKey(e => e.PatientId);

    modelBuilder.Entity<Doctor>()
        .HasMany(e => e.Appointments)
        .WithOne(e => e.Doctor)
        .HasForeignKey(e => e.DoctorId);

    modelBuilder.Entity<Appointment>()
        .HasMany(e => e.AppointmentServices)
        .WithOne(e => e.Appointment)
        .HasForeignKey(e => e.AppointmentId);

    modelBuilder.Entity<MedicalService>()
        .HasMany(e => e.AppointmentServices)
        .WithOne(e => e.MedicalService)
        .HasForeignKey(e => e.ServiceId);

    modelBuilder.Entity<Doctor>().HasData(
        new Doctor
        {
            DoctorId = 1,
            FirstName = "Anna",
            LastName = "Nowak",
            Specialization = "Cardiology",
            Phone = "123456789"
        },
        new Doctor
        {
            DoctorId = 2,
            FirstName = "Jan",
            LastName = "Kowalski",
            Specialization = "Dermatology",
            Phone = "987654321"
        },
        new Doctor
        {
            DoctorId = 3,
            FirstName = "Maria",
            LastName = "Wisniewska",
            Specialization = "Neurology",
            Phone = "555666777"
        }
    );

    modelBuilder.Entity<Patient>().HasData(
        new Patient
        {
            PatientId = 1,
            FirstName = "Karolina",
            LastName = "Kowalska",
            DateOfBirth = new DateTime(1998, 4, 12),
            Phone = "111222333"
        },
        new Patient
        {
            PatientId = 2,
            FirstName = "Tomasz",
            LastName = "Nowicki",
            DateOfBirth = new DateTime(1987, 11, 3),
            Phone = "222333444"
        },
        new Patient
        {
            PatientId = 3,
            FirstName = "Ewa",
            LastName = "Maj",
            DateOfBirth = new DateTime(1975, 7, 21),
            Phone = "333444555"
        }
    );
    }
    
}