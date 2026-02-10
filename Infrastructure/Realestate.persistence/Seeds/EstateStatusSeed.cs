using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Property;
namespace Realestate.persistence.Seeds;

/// <summary>
/// Seeds reference data for EstateStatus (Property schema).
/// </summary>
public class EstateStatusSeed : IEntityTypeConfiguration<EstateStatus>
{
    public void Configure(EntityTypeBuilder<EstateStatus> builder)
    {
        builder.HasData(
            new EstateStatus { Id = 1, Name = "Available", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateStatus { Id = 2, Name = "Sold", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateStatus { Id = 3, Name = "Under Contract", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateStatus { Id = 4, Name = "Pending", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateStatus { Id = 5, Name = "Rented", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new EstateStatus { Id = 6, Name = "Off Market", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}