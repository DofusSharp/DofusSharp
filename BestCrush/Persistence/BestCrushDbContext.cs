using BestCrush.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace BestCrush.Persistence;

class BestCrushDbContext : DbContext
{
    public BestCrushDbContext(DbContextOptions<BestCrushDbContext> options) : base(options) { }

    public DbSet<CurrentVersion> CurrentVersions { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemPriceRecord> ItemPriceRecords { get; set; }
    public DbSet<ItemCoefficientRecord> ItemCoefficientRecords { get; set; }
    public DbSet<RunePriceRecord> RunePriceRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemPriceRecord>().HasIndex(r => r.ItemId);
        modelBuilder.Entity<ItemPriceRecord>().HasIndex(r => r.ServerName);

        modelBuilder.Entity<ItemCoefficientRecord>().HasIndex(r => r.ItemId);
        modelBuilder.Entity<ItemCoefficientRecord>().HasIndex(r => r.ServerName);

        modelBuilder.Entity<RunePriceRecord>().HasIndex(r => r.RuneId);
        modelBuilder.Entity<RunePriceRecord>().HasIndex(r => r.ServerName);
    }
}
