using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Realestate.Domain.Entities.Identity;
using Realestate.persistence.Contexts;
namespace Realestate.IntegrationTests;

public class RealEstateWebAppFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _connection;

    public RealEstateWebAppFactory()
    {
        // Keep a single SQLite in-memory connection alive for the lifetime of the factory
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove all DbContext registrations
            var descriptors = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                         || d.ServiceType == typeof(ApplicationDbContext))
                .ToList();
            foreach (var d in descriptors)
                services.Remove(d);

            // Add SQLite in-memory database (relational — supports Dapper + GetDbConnection)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(_connection));

            // Build the service provider and seed data
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();

            SeedTestData(db);
        });

        builder.UseEnvironment("Development");
    }

    private static void SeedTestData(ApplicationDbContext db)
    {
        // Staff roles
        if (!db.StaffRoles.Any())
        {
            db.StaffRoles.AddRange(
                new StaffRole { Id = 1, Name = "Admin" },
                new StaffRole { Id = 2, Name = "Agent" }
            );
            db.SaveChanges();
        }

        // A pre-existing staff member with known password for login tests
        if (!db.Staffs.Any())
        {
            db.Staffs.Add(new Staff
            {
                Id = 1,
                Name = "Test",
                Surname = "User",
                Email = "test@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Test1234"),
                Code = "T001",
                StaffRoleId = 1
            });
            db.SaveChanges();
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
            _connection.Dispose();
    }
}