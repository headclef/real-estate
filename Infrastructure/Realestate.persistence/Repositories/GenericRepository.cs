using System.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Realestate.Application.Interfaces.Repositories;
using Realestate.persistence.Contexts;
using Realestate.Domain.Entities.Commons;
namespace Realestate.persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDbConnection _dbConnection;
    private readonly string _tableName;

    public GenericRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbConnection = dbContext.Database.GetDbConnection();
        _tableName = typeof(T).Name; // Simplified pluralization, adjust if necessary
        // Note: We use entity type name as table name, ensure it matches your actual table names or implement a mapping strategy if needed.
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM {_tableName} WHERE Id = @Id AND IsDeleted = 0";
        return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM {_tableName} WHERE IsDeleted = 0";
        return await _dbConnection.QueryAsync<T>(sql);
    }

    public async Task<int> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        entity.IsDeleted = true;
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }
}