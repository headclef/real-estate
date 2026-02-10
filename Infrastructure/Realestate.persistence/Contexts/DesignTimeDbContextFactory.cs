using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Realestate.persistence.Contexts;

/// <summary>
/// Enables EF Core CLI tools (dotnet ef migrations add/update) to create the DbContext
/// when running from the Infrastructure project while the connection string lives in the API project.
/// Usage:  dotnet ef migrations add InitialCreate --project Infrastructure\Realestate.persistence --startup-project Web\Realestate.API
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Navigate from the persistence project to the API project to read appsettings.json
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Web", "Realestate.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.GetFullPath(basePath))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}