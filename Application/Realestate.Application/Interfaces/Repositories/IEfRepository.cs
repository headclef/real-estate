using Realestate.Domain.Entities.Commons;
namespace Realestate.Application.Interfaces.Repositories;

public interface IEfRepository<T> where T : BaseEntity
{
    Task<int> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}