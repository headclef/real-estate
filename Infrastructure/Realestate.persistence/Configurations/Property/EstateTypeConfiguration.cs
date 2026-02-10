using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Property;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.Property;

/// <summary>
/// EF Core fluent configuration for the EstateType entity.
/// Schema: Property | Table: EstateType
/// </summary>
public class EstateTypeConfiguration : BaseEntityConfiguration<EstateType>
{
    public override void Configure(EntityTypeBuilder<EstateType> builder)
    {
        base.Configure(builder);

        builder.ToTable("EstateType", "Property");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Relationships
        builder.HasMany(e => e.Estates)
            .WithOne(e => e.EstateType)
            .HasForeignKey(e => e.EstateTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}