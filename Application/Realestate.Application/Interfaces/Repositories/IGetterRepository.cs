using Realestate.Domain.Entities.Commons;
namespace Realestate.Application.Interfaces.Repositories;

public interface IGetterRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}