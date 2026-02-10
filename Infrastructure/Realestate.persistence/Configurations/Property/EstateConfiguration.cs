using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.Property;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.Property;

/// <summary>
/// EF Core fluent configuration for the Estate entity.
/// Schema: Property | Table: Estate
/// </summary>
public class EstateConfiguration : BaseEntityConfiguration<Estate>
{
    public override void Configure(EntityTypeBuilder<Estate> builder)
    {
        base.Configure(builder);

        builder.ToTable("Estate", "Property");

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(2000);

        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        // Foreign Keys
        builder.Property(e => e.DivisionId)
            .IsRequired();

        builder.Property(e => e.EstateTypeId)
            .IsRequired();

        builder.Property(e => e.EstateStatusId)
            .IsRequired();

        // Division relationship (cross-schema: Property → World)
        builder.HasOne(e => e.Division)
            .WithMany()
            .HasForeignKey(e => e.DivisionId)
            .OnDelete(DeleteBehavior.Restrict);

        // EstateType and EstateStatus relationships are configured from the parent side
        // (EstateTypeConfiguration and EstateStatusConfiguration)
    }
}