using Realestate.Application.Interfaces.Repositories.StaffRole;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.Identity;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class StaffRoleRepository : GenericRepository<StaffRole>, IStaffRoleRepository
{
    public StaffRoleRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}