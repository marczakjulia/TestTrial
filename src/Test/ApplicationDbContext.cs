using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Competition> Competition { get; set; }
    public DbSet<Driver> Driver { get; set; }
    public DbSet<DriverCompetition> DriverCompetition { get; set; }
    public DbSet<Car> Car { get; set; }
    public DbSet<CarManufacturer> CarManufacturer { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DriverCompetition>()
            .HasKey(dc => new { dc.DriverId, dc.CompetitionId });
        modelBuilder.Entity<DriverCompetition>()
            .HasOne(dc => dc.Driver)
            .WithMany(d => d.DriverCompetitions)
            .HasForeignKey(dc => dc.DriverId);

        modelBuilder.Entity<DriverCompetition>()
            .HasOne(dc => dc.Competition)
            .WithMany(c => c.DriverCompetitions)
            .HasForeignKey(dc => dc.CompetitionId);

        base.OnModelCreating(modelBuilder);
    }
}