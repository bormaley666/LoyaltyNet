using System;
using System.Linq;
using LoyaltyNet.Domain;
using LoyaltyNet.Domain.References.Categories;
using LoyaltyNet.Domain.References.Mccs;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyNet.Persistence;

/// <inheritdoc/>
public sealed class AppDbContext : DbContext
{
    /// <summary>
    /// MCC-коды
    /// </summary>
    public DbSet<Mcc> Mcc { get; set; } = default!;

    /// <summary>
    /// MCC-категории
    /// </summary>
    public DbSet<Category> Categories { get; set; } = default!;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="options"></param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) 
    {
        SavingChanges += OnSavingChanges;
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private void OnSavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var entries = ChangeTracker.Entries()
            .Where(entry => entry.State == EntityState.Modified || entry.State == EntityState.Added);

        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditEntity entity)
            {
                entity.UpdatedAt = DateTimeOffset.UtcNow;
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = entity.UpdatedAt;
                }
            }
        }
    }
}