using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Template.Entities;

[Table("Doctors")]
public class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Specialization { get; set; } = string.Empty;

    [MaxLength(9)]
    [Column(TypeName = "varchar(9)")]
    public string? Phone { get; set; }

    public IEnumerable<Appointment> Appointments { get; set; } = [];
}