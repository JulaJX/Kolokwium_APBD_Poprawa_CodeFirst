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
    }
}