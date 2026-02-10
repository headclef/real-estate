using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Property;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.Property;

/// <summary>
/// EF Core fluent configuration for the EstateStatus entity.
/// Schema: Property | Table: EstateStatuse
/// </summary>
public class EstateStatusConfiguration : BaseEntityConfiguration<EstateStatus>
{
    public override void Configure(EntityTypeBuilder<EstateStatus> builder)
    {
        base.Configure(builder);

        builder.ToTable("EstateStatuse", "Property");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Relationships
        builder.HasMany(e => e.Estates)
            .WithOne(e => e.EstateStatus)
            .HasForeignKey(e => e.EstateStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}