using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Property;
namespace Realestate.persistence.Seeds;

/// <summary>
/// Seeds reference data for EstateType (Property schema).
/// </summary>
public class EstateTypeSeed : IEntityTypeConfiguration<EstateType>
{
    public void Configure(EntityTypeBuilder<EstateType> builder)
    {
        builder.HasData(
            new EstateType { Id = 1, Name = "Residential", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateType { Id = 2, Name = "Commercial", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateType { Id = 3, Name = "Industrial", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateType { Id = 4, Name = "Land", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateType { Id = 5, Name = "Mixed-Use", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}