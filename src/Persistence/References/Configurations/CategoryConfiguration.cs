using LoyaltyNet.Domain.References.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoyaltyNet.Persistence.References.Configurations;

/// <summary>
/// Конфигурация <see cref="MccCategory"/>
/// </summary>
internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("mcc_category",
            builder =>
            {
                builder.HasCheckConstraint("valid_dates", "valid_to IS NULL OR valid_from < valid_to");
                builder.HasCheckConstraint("valid_code", "code >= 0");
            });

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Code)
            .HasColumnName("code")
            .HasConversion(
                v => v.Value,
                v => new CategoryCode(v)
            );

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.Description)
            .HasColumnName("description");

        builder.Property(e => e.ValidFrom)
            .HasColumnName("valid_from");

        builder.Property(e => e.ValidTo)
            .HasColumnName("valid_to");

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(e => e.UpdatedAt)
            .HasColumnName("updated_at");
    }
}
