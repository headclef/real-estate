using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Identity;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.Identity;

/// <summary>
/// EF Core fluent configuration for the StaffRole entity.
/// Schema: Identity | Table: StaffRole
/// </summary>
public class StaffRoleConfiguration : BaseEntityConfiguration<StaffRole>
{
    public override void Configure(EntityTypeBuilder<StaffRole> builder)
    {
        base.Configure(builder);

        builder.ToTable("StaffRole", "Identity");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Relationships
        builder.HasMany(e => e.Staffs)
            .WithOne(s => s.StaffRole)
            .HasForeignKey(s => s.StaffRoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}