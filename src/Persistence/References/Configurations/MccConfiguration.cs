using LoyaltyNet.Domain.References.Mccs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyNet.Persistence.References.Configurations;

/// <summary>
/// Конфигурация <see cref="Mcc"/>
/// </summary>
internal sealed class MccConfiguration : IEntityTypeConfiguration<Mcc>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Mcc> builder)
    {
        builder.ToTable("mcc",
            builder =>
            {
                builder.HasCheckConstraint("valid_dates", "valid_to IS NULL OR valid_from < valid_to");
                builder.HasCheckConstraint("valid_code", "code ~ '^[0-9]{4}$'");
            });

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Code)
            .HasColumnName("code")
            .HasMaxLength(4)
            .HasConversion(
                v => v.Value,
                v => new MccCode(v)
            );

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.CategoryId)
            .HasColumnName("category_id");

        builder.Property(e => e.ValidFrom)
            .HasColumnName("valid_from");

        builder.Property(e => e.ValidTo)
            .HasColumnName("valid_to");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");

        builder.HasOne(e => e.Category)
            .WithMany(c => c.Mcc)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.Code)
            .IsUnique()
            .HasFilter("valid_to IS NULL");
    }
}
