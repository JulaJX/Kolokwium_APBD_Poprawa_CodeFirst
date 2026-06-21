using APBD_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD_Template.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> opt) : DbContext(opt)
{
    public DbSet<Racer> Racers { get; set; }
    public DbSet<RacePartcipation> RacePartcipations { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Race> Races { get; set; }

   
}