using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Template.Entities;

    
[Table("Racer")]
public class Racer
{
    [Key]
    public int RacerId { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string FirstName { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; } = string.Empty;
    

    public IEnumerable<RaceParticipation> RaceParticipation { get; set; } = [];

}