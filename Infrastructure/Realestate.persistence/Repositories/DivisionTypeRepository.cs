using Realestate.Application.Interfaces.Repositories.DivisionType;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.World;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class DivisionTypeRepository : GenericRepository<DivisionType>, IDivisionTypeRepository
{
    public DivisionTypeRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}