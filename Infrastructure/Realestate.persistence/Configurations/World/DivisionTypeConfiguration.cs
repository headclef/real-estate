using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.World;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.World;

/// <summary>
/// EF Core fluent configuration for the DivisionType entity.
/// Schema: World | Table: DivisionType
/// </summary>
public class DivisionTypeConfiguration : BaseEntityConfiguration<DivisionType>
{
    public override void Configure(EntityTypeBuilder<DivisionType> builder)
    {
        base.Configure(builder);

        builder.ToTable("DivisionType", "World");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        // Relationships
        builder.HasMany(e => e.Divisions)
            .WithOne(d => d.DivisionType)
            .HasForeignKey(d => d.DivisionTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}