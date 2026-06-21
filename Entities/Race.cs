using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Template.Entities;

[Table("Race")]
public class Race
{
    [Key]
    public int RaceId { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(100)]
    [Column(TypeName = "nvarchar(100)")]
    public string Location { get; set; } = string.Empty;
    
    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }
    
    public IEnumerable<TrackRace> TrackRaces { get; set; } = [];
}