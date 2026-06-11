using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Template.Entities;

[Table("Appointment_Services")]
[PrimaryKey(nameof(AppointmentId), nameof(ServiceId))]
public class AppointmentService
{
    public int AppointmentId { get; set; }

    public int ServiceId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "date")]
    public DateTime PerformedAt { get; set; }

    [ForeignKey(nameof(AppointmentId))]
    public Appointment Appointment { get; set; } = null!;

    [ForeignKey(nameof(ServiceId))]
    public MedicalService MedicalService { get; set; } = null!;
}