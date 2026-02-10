using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Identity;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.Identity;

/// <summary>
/// EF Core fluent configuration for the Staff entity.
/// Schema: Identity | Table: Staff
/// </summary>
public class StaffConfiguration : BaseEntityConfiguration<Staff>
{
    public override void Configure(EntityTypeBuilder<Staff> builder)
    {
        base.Configure(builder);

        builder.ToTable("Staff", "Identity");

        builder.Property(e => e.Name)
            .HasMaxLength(100);

        builder.Property(e => e.Surname)
            .HasMaxLength(100);

        // FullName is a computed (read-only) property — ignore it so EF doesn't map it to a column
        builder.Ignore(e => e.FullName);

        builder.Property(e => e.Code)
            .HasMaxLength(20);

        builder.Property(e => e.Email)
            .HasMaxLength(200);

        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(30);

        // Foreign Keys
        builder.Property(e => e.StaffRoleId)
            .IsRequired();

        // StaffRole relationship is configured from the parent side (StaffRoleConfiguration)
    }
}