using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.World;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.World;

/// <summary>
/// EF Core fluent configuration for the Division entity.
/// Schema: World | Table: Division
/// </summary>
public class DivisionConfiguration : BaseEntityConfiguration<Division>
{
    public override void Configure(EntityTypeBuilder<Division> builder)
    {
        base.Configure(builder);

        builder.ToTable("Division", "World");

        builder.Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.Code)
            .HasMaxLength(20);

        // Foreign Keys
        builder.Property(e => e.CountryId)
            .IsRequired();

        builder.Property(e => e.DivisionTypeId)
            .IsRequired();

        builder.Property(e => e.ParentId)
            .IsRequired(false);

        // Self-referencing relationship (Parent → Children)
        builder.HasOne(e => e.ParentDivision)
            .WithMany()
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Country and DivisionType relationships are configured from the parent side
        // (CountryConfiguration and DivisionTypeConfiguration)
    }
}