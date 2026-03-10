using Microsoft.EntityFrameworkCore;
using FeatureFlags.Api.Entities;
using System.Data;

namespace FeatureFlags.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<Entities.Environment> Environments => Set<Entities.Environment>();
    public DbSet<Entities.Rule> Rules => Set<Entities.Rule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feature>()
            .HasIndex(f => f.Key)
            .IsUnique();

        modelBuilder.Entity<Entities.Environment>()
            .HasIndex(e => e.Slug)
            .IsUnique();

        modelBuilder.Entity<Entities.Rule>()
           .HasIndex(e => e.Id)
           .IsUnique();
    }
}