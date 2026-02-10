using Realestate.Application.Interfaces.Repositories.EstateType;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.Property;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class EstateTypeRepository : GenericRepository<EstateType>, IEstateTypeRepository
{
    public EstateTypeRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}