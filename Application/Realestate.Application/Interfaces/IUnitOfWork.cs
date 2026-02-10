using System.Data;
using Realestate.Application.Interfaces.Repositories.Country;
using Realestate.Application.Interfaces.Repositories.Division;
using Realestate.Application.Interfaces.Repositories.DivisionType;
using Realestate.Application.Interfaces.Repositories.Estate;
using Realestate.Application.Interfaces.Repositories.EstateStatus;
using Realestate.Application.Interfaces.Repositories.EstateType;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Interfaces.Repositories.StaffRole;
namespace Realestate.Application.Interfaces;

/// <summary>
/// Unit of Work pattern — coordinates writes across multiple repositories within a single transaction.
/// Dapper reads use the shared Connection; EF Core writes are flushed via SaveChangesAsync.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // Repositories — World
    ICountryRepository Countries { get; }
    IDivisionTypeRepository DivisionTypes { get; }
    IDivisionRepository Divisions { get; }

    // Repositories — Property
    IEstateTypeRepository EstateTypes { get; }
    IEstateStatusRepository EstateStatuses { get; }
    IEstateRepository Estates { get; }

    // Repositories — Identity
    IStaffRoleRepository StaffRoles { get; }
    IStaffRepository Staffs { get; }

    /// <summary>
    /// Persists all tracked EF Core changes to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins an explicit database transaction (useful when coordinating multiple writes).
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction.
    /// </summary>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current transaction.
    /// </summary>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}