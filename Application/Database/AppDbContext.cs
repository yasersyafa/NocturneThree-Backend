using Backend.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Application.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Player> Players { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<OtpCode> OtpCodes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Player>()
            .HasIndex(p => p.Email)
            .IsUnique();

        modelBuilder.Entity<Player>()
            .HasIndex(p => p.Username)
            .IsUnique();

        modelBuilder.Entity<Session>()
            .HasIndex(s => s.Token)
            .IsUnique();

        modelBuilder.Entity<OtpCode>()
            .HasIndex(o => new { o.Email, o.Code, o.Consumed, o.Purpose });
    }
}