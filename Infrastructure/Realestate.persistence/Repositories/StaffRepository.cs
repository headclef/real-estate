using System.Data;
using Dapper;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.Identity;
using Realestate.persistence.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Realestate.persistence.Repositories;

public class StaffRepository : GenericRepository<Staff>, IStaffRepository
{
    private readonly IDbConnection _connection;
    private readonly string _table;

    public StaffRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _connection = dbContext.Database.GetDbConnection();
        var entityType = dbContext.Model.FindEntityType(typeof(Staff));
        var schema = entityType?.GetSchema() ?? "Identity";
        var table = entityType?.GetTableName() ?? "Staff";
        _table = $"[{schema}].[{table}]";
    }

    public async Task<Staff?> GetByEmailAsync(string email)
    {
        var sql = $"SELECT * FROM {_table} WHERE Email = @Email AND IsDeleted = 0";
        return await _connection.QueryFirstOrDefaultAsync<Staff>(sql, new { Email = email });
    }
}