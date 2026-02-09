using Realestate.Application.Interfaces.Repositories.Country;
using Realestate.Domain.Entities;
using Realestate.persistence.Contexts;
namespace Realestate.persistence.Repositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(ApplicationDbContext dbContext) : base(dbContext) { }
}