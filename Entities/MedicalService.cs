using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Template.Entities;

[Table("Medical_Services")]
public class MedicalService
{
    [Key]
    public int ServiceId { get; set; }

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    public int DurationMinutes { get; set; }

    public IEnumerable<AppointmentService> AppointmentServices { get; set; } = [];
}