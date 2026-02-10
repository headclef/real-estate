using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Repositories.Country;
using Realestate.Application.Interfaces.Repositories.Division;
using Realestate.Application.Interfaces.Repositories.DivisionType;
using Realestate.Application.Interfaces.Repositories.Estate;
using Realestate.Application.Interfaces.Repositories.EstateStatus;
using Realestate.Application.Interfaces.Repositories.EstateType;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Interfaces.Repositories.StaffRole;
using Realestate.persistence.Contexts;
using Realestate.persistence.Repositories;
namespace Realestate.persistence;

/// <summary>
/// Coordinates EF Core writes and Dapper reads across all repositories within a single scope.
/// Repositories are lazily instantiated so only the ones actually used consume resources.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    // Lazy-loaded repository backing fields
    private ICountryRepository? _countries;
    private IDivisionTypeRepository? _divisionTypes;
    private IDivisionRepository? _divisions;
    private IEstateTypeRepository? _estateTypes;
    private IEstateStatusRepository? _estateStatuses;
    private IEstateRepository? _estates;
    private IStaffRoleRepository? _staffRoles;
    private IStaffRepository? _staffs;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // ────── Repository Properties (lazy) ──────

    // World
    public ICountryRepository Countries =>
        _countries ??= new CountryRepository(_dbContext);

    public IDivisionTypeRepository DivisionTypes =>
        _divisionTypes ??= new DivisionTypeRepository(_dbContext);

    public IDivisionRepository Divisions =>
        _divisions ??= new DivisionRepository(_dbContext);

    // Property
    public IEstateTypeRepository EstateTypes =>
        _estateTypes ??= new EstateTypeRepository(_dbContext);

    public IEstateStatusRepository EstateStatuses =>
        _estateStatuses ??= new EstateStatusRepository(_dbContext);

    public IEstateRepository Estates =>
        _estates ??= new EstateRepository(_dbContext);

    // Identity
    public IStaffRoleRepository StaffRoles =>
        _staffRoles ??= new StaffRoleRepository(_dbContext);

    public IStaffRepository Staffs =>
        _staffs ??= new StaffRepository(_dbContext);

    // ────── Persistence ──────

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    // ────── Transaction Management ──────

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    // ────── Dispose ──────

    public void Dispose()
    {
        _transaction?.Dispose();
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}