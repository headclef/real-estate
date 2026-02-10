using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Repositories;
using Realestate.Application.Interfaces.Repositories.Country;
using Realestate.Application.Interfaces.Repositories.Division;
using Realestate.Application.Interfaces.Repositories.DivisionType;
using Realestate.Application.Interfaces.Repositories.Estate;
using Realestate.Application.Interfaces.Repositories.EstateStatus;
using Realestate.Application.Interfaces.Repositories.EstateType;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Interfaces.Repositories.StaffRole;
using Realestate.persistence.Contexts;
using Realestate.persistence.Repositories;
namespace Realestate.persistence.Registration;

public static class ServiceRegistration
{
    public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        #region Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        #endregion

        #region Repositories
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IDivisionRepository, DivisionRepository>();
        services.AddScoped<IDivisionTypeRepository, DivisionTypeRepository>();
        services.AddScoped<IEstateRepository, EstateRepository>();
        services.AddScoped<IEstateStatusRepository, EstateStatusRepository>();
        services.AddScoped<IEstateTypeRepository, EstateTypeRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IStaffRoleRepository, StaffRoleRepository>();
        #endregion
    }
}