
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Template.Entities;

[Table("Race_Participation")]
[PrimaryKey(nameof(TrackRaceId), nameof(RacerId))]
public class RaceParticipation
{
    public int TrackRaceId { get; set; }
    public int RacerId { get; set; }
    public int FinishTmeInSeconds { get; set; }
    public int Position { get; set; }

    [ForeignKey(nameof(TrackRaceId))]
    public TrackRace TrackRace { get; set; } = null!;

    [ForeignKey(nameof(RacerId))]
    public Racer Racer { get; set; } = null!;

}