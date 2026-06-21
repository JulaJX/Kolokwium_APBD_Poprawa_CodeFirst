using APBD_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace APBD_Template.Data;

public class DatabaseContext(DbContextOptions<DatabaseContext> opt) : DbContext(opt)
{
    public DbSet<Racer> Racers { get; set; }
    public DbSet<RaceParticipation> RacePartcipations { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<Race> Races { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RaceParticipation>()
            .HasKey(e => new { e.TrackRaceId, e.RacerId });

        modelBuilder.Entity<Racer>()
            .HasMany(e => e.RaceParticipations)
            .WithOne(e => e.Racer)
            .HasForeignKey(e => e.RacerId);

        modelBuilder.Entity<TrackRace>()
            .HasMany(e => e.RaceParticipations)
            .WithOne(e => e.TrackRace)
            .HasForeignKey(e => e.TrackRaceId);

        modelBuilder.Entity<Track>()
            .HasMany(e => e.TrackRaces)
            .WithOne(e => e.Track)
            .HasForeignKey(e => e.TrackId);

        modelBuilder.Entity<Race>()
            .HasMany(e => e.TrackRaces)
            .WithOne(e => e.Race)
            .HasForeignKey(e => e.RaceId);
        
    }
}