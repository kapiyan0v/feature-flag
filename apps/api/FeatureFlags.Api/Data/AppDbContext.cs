using Microsoft.EntityFrameworkCore;
using FeatureFlags.Api.Entities;

namespace FeatureFlags.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<Entities.Environment> Environments => Set<Entities.Environment>();
    public DbSet<Rule> Rules => Set<Rule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feature>()
            .HasIndex(f => f.Key)
            .IsUnique();

        modelBuilder.Entity<Entities.Environment>()
            .HasIndex(e => e.Slug)
            .IsUnique();
    }
}