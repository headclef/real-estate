using Realestate.Application.Interfaces.Repositories.EstateStatus;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.Property;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class EstateStatusRepository : GenericRepository<EstateStatus>, IEstateStatusRepository
{
    public EstateStatusRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}