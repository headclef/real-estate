using Realestate.Application.Interfaces.Repositories.Division;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.World;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class DivisionRepository : GenericRepository<Division>, IDivisionRepository
{
    public DivisionRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}