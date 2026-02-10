using Microsoft.Extensions.DependencyInjection;
using Realestate.Application.Interfaces.Services.Auth;
using Realestate.Application.Interfaces.Services.Country;
using Realestate.Application.Interfaces.Services.Division;
using Realestate.Application.Interfaces.Services.DivisionType;
using Realestate.Application.Interfaces.Services.Estate;
using Realestate.Application.Interfaces.Services.EstateStatus;
using Realestate.Application.Interfaces.Services.EstateType;
using Realestate.Application.Interfaces.Services.Staff;
using Realestate.Application.Interfaces.Services.StaffRole;
using Realestate.Business.Services;
namespace Realestate.Business.Registration;

public static class ServiceRegistration
{
    public static void AddBusinessLayer(this IServiceCollection services)
    {
        // Auth
        services.AddScoped<IAuthService, AuthService>();

        // World
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDivisionTypeService, DivisionTypeService>();
        services.AddScoped<IDivisionService, DivisionService>();

        // Property
        services.AddScoped<IEstateTypeService, EstateTypeService>();
        services.AddScoped<IEstateStatusService, EstateStatusService>();
        services.AddScoped<IEstateService, EstateService>();

        // Identity
        services.AddScoped<IStaffRoleService, StaffRoleService>();
        services.AddScoped<IStaffService, StaffService>();
    }
}