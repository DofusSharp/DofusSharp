using BestCrush.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BestCrush.Domain;

public class BestCrushDbContext : DbContext
{
    public BestCrushDbContext(DbContextOptions<BestCrushDbContext> options) : base(options) { }

    public DbSet<Upgrade> Upgrades { get; set; }

    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Rune> Runes { get; set; }
    public DbSet<Resource> Resources { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipment>().HasMany(e => e.Characteristics).WithOne(e => e.Equipment).OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Equipment>().HasMany(e => e.Recipe).WithOne(e => e.Equipment).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Equipment>().HasAlternateKey(u => u.DofusDbId);
        modelBuilder.Entity<Rune>().HasAlternateKey(u => u.DofusDbId);
        modelBuilder.Entity<Resource>().HasAlternateKey(u => u.DofusDbId);
    }
}
