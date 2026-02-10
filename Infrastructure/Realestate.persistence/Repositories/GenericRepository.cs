using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Realestate.Application.Interfaces.Repositories;
using Realestate.persistence.Contexts;
using Realestate.Domain.Entities.Commons;
namespace Realestate.persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly ApplicationDbContext _dbContext;
    private readonly IDbConnection _dbConnection;
    private readonly string _qualifiedTableName;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbConnection = dbContext.Database.GetDbConnection();

        // Resolve [Schema].[Table] from the EF model so Dapper queries match the fluent config
        var entityType = dbContext.Model.FindEntityType(typeof(T));
        var schema = entityType?.GetSchema() ?? "dbo";
        var table = entityType?.GetTableName() ?? typeof(T).Name;
        _qualifiedTableName = $"[{schema}].[{table}]";
    }

    // ────── Dapper Reads ──────

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM {_qualifiedTableName} WHERE Id = @Id AND IsDeleted = 0";
        return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM {_qualifiedTableName} WHERE IsDeleted = 0";
        return await _dbConnection.QueryAsync<T>(sql);
    }

    // ────── EF Core Writes (no SaveChanges — UnitOfWork flushes) ──────

    public async Task<int> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity.Id;
    }

    public Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        _dbContext.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}