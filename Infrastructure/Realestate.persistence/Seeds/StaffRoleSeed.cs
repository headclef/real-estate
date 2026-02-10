using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Identity;
namespace Realestate.persistence.Seeds;

/// <summary>
/// Seeds reference data for StaffRole (Identity schema).
/// </summary>
public class StaffRoleSeed : IEntityTypeConfiguration<StaffRole>
{
    public void Configure(EntityTypeBuilder<StaffRole> builder)
    {
        builder.HasData(
            new StaffRole { Id = 1, Name = "Administrator", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new StaffRole { Id = 2, Name = "Manager", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new StaffRole { Id = 3, Name = "Agent", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new StaffRole { Id = 4, Name = "Analyst", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new StaffRole { Id = 5, Name = "Support", CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}