using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Domain.Entities.Identity;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class StaffRepository : GenericRepository<Staff>, IStaffRepository
{
    public StaffRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}