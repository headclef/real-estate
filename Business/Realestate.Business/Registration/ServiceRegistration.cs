using Microsoft.Extensions.DependencyInjection;
using Realestate.Application.Interfaces.Services.Country;
using Realestate.Business.Services;
namespace Realestate.Business.Registration;

public static class ServiceRegistration
{
    public static void AddBusinessLayer(this IServiceCollection services)
    {
        services.AddScoped<ICountryService, CountryService>();
        // Add other services here as they are implemented
    }
}