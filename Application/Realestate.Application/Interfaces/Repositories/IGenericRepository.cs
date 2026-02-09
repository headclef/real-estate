using Realestate.Domain.Entities.Commons;
namespace Realestate.Application.Interfaces.Repositories;

public interface IGenericRepository<T> : IGetterRepository<T>, IEfRepository<T> where T : BaseEntity
{
}