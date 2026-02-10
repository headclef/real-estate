using Realestate.Application.Interfaces.Repositories.Estate;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.Property;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class EstateRepository : GenericRepository<Estate>, IEstateRepository
{
    public EstateRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}