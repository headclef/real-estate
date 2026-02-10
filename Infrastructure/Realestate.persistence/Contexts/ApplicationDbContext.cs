using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Realestate.Domain.Entities.Commons;
using Realestate.Domain.Entities.World;
using Realestate.Domain.Entities.Property;
using Realestate.Domain.Entities.Identity;
namespace Realestate.persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // World
    public DbSet<Country> Countries { get; set; }
    public DbSet<Division> Divisions { get; set; }
    public DbSet<DivisionType> DivisionTypes { get; set; }

    // Property
    public DbSet<Estate> Estates { get; set; }
    public DbSet<EstateStatus> EstateStatuses { get; set; }
    public DbSet<EstateType> EstateTypes { get; set; }

    // Identity
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<StaffRole> StaffRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all IEntityTypeConfiguration<T> from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Global Query Filter for Soft Delete
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(ConvertFilterExpression(entityType.ClrType));
            }
        }
    }

    private static System.Linq.Expressions.LambdaExpression ConvertFilterExpression(Type type)
    {
        var parameter = System.Linq.Expressions.Expression.Parameter(type, "e");
        var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
        var falseConstant = System.Linq.Expressions.Expression.Constant(false);
        var body = System.Linq.Expressions.Expression.Equal(property, falseConstant);
        return System.Linq.Expressions.Expression.Lambda(body, parameter);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}