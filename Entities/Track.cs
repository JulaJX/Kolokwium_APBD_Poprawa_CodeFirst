using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Template.Entities;
[Table("Track")]
public class Track
{
    [Key]
    public int TrackId { get; set; }

    [MaxLength(100)]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(5,2)")]
    public int LengthInKm { get; set; }
    

    public IEnumerable<TrackRace> TrackRace { get; set; } = [];
}