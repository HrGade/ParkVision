using Microsoft.EntityFrameworkCore;
using ParkVision.Server.Model;

namespace ParkVision.Server.DB;

public class ParkVisionDbContext : DbContext
{
    public DbSet<Bil> Biler { get; set; } = default!;
    public DbSet<Parkeringsplads> Parkeringspladser { get; set; } = default!;
    public DbSet<Parkering> Parkeringer { get; set; } = default!;

    public ParkVisionDbContext(DbContextOptions<ParkVisionDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "server=mssql8.unoeuro.com;Database=adamgadefrimann_dk_db_parkvision;User Id=adamgadefrimann_dk;Password=EAez96yf4RD5xphGb3wg");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bil>()
            .ToTable(nameof(Bil))
            .HasKey(nameof(Bil.Nummerplade));

        modelBuilder.Entity<Parkeringsplads>()
            .ToTable(nameof(Parkeringsplads))
            .HasKey(nameof(Parkeringsplads.ParkeringspladsID));

        modelBuilder.Entity<Parkering>()
            .ToTable(nameof(Parkering))
            .HasOne(e => e.Parkeringsplads)
            .WithMany()
            .HasForeignKey(c => new { c.Nummerplade })
            .HasForeignKey(u => new { u.ParkeringspladsID })
            .OnDelete(DeleteBehavior.Cascade);
    }
}
