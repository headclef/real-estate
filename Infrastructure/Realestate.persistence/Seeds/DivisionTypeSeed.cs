using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.World;
namespace Realestate.persistence.Seeds;

/// <summary>
/// Seeds reference data for DivisionType (World schema).
/// These are structural/lookup values that rarely change.
/// </summary>
public class DivisionTypeSeed : IEntityTypeConfiguration<DivisionType>
{
    public void Configure(EntityTypeBuilder<DivisionType> builder)
    {
        builder.HasData(
            new DivisionType { Id = 1, Name = "Region", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new DivisionType { Id = 2, Name = "State", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new DivisionType { Id = 3, Name = "Province", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new DivisionType { Id = 4, Name = "City", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new DivisionType { Id = 5, Name = "District", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new DivisionType { Id = 6, Name = "Neighborhood", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}