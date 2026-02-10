using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Realestate.Domain.Entities.World;
using Realestate.persistence.Configurations.Commons;
namespace Realestate.persistence.Configurations.World;

/// <summary>
/// EF Core fluent configuration for the Country entity.
/// Schema: World | Table: Country
/// </summary>
public class CountryConfiguration : BaseEntityConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);

        builder.ToTable("Country", "World");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.IsoTwo)
            .HasMaxLength(2);

        builder.Property(e => e.IsoThree)
            .HasMaxLength(3);

        builder.Property(e => e.IsoNumber)
            .HasMaxLength(3);

        builder.Property(e => e.Cctld)
            .HasMaxLength(10);

        builder.Property(e => e.Plate)
            .HasMaxLength(5);

        builder.Property(e => e.Currency)
            .HasMaxLength(5);

        // Relationships
        builder.HasMany(e => e.Divisions)
            .WithOne(d => d.Country)
            .HasForeignKey(d => d.CountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
