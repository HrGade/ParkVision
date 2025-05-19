using Microsoft.EntityFrameworkCore;
using ParkVision.Server.Model;

namespace ParkVision.Server.DB;

public class BilDbContext : DbContext
{
    public DbSet<Bil> Biler { get; set; }

    public BilDbContext(DbContextOptions<BilDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "server=(localdb)\\MSSQLLocalDB;database=ParkVisionDB;trusted_connection=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bil>()
            .ToTable("Bil")
            .HasKey(nameof(Bil.Nummerplade));
    }
}
