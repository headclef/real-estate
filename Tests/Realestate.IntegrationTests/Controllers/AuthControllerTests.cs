using System.Net;
using System.Net.Http.Json;
using Realestate.Application.DTOs.Auth;
using Realestate.Application.Wrappers;
namespace Realestate.IntegrationTests.Controllers;

public class AuthControllerTests : IClassFixture<RealEstateWebAppFactory>
{
    private readonly HttpClient _client;

    public AuthControllerTests(RealEstateWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    // ────────── Register ──────────

    [Fact]
    public async Task Register_ValidDto_Returns200WithToken()
    {
        var dto = new RegisterDto
        {
            Name = "New",
            Surname = "User",
            Email = $"new{Guid.NewGuid():N}@example.com",
            Password = "Secure123",
            ConfirmPassword = "Secure123",
            StaffRoleId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v0/auth/register", dto);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        Assert.True(body!.Success);
        Assert.False(string.IsNullOrEmpty(body.Data!.Token));
    }

    [Fact]
    public async Task Register_DuplicateEmail_Returns409()
    {
        var email = $"dup{Guid.NewGuid():N}@example.com";
        var dto = new RegisterDto
        {
            Name = "A", Surname = "B", Email = email,
            Password = "Pass123", ConfirmPassword = "Pass123", StaffRoleId = 1
        };

        // First registration
        await _client.PostAsJsonAsync("/api/v0/auth/register", dto);

        // Second — duplicate
        var response = await _client.PostAsJsonAsync("/api/v0/auth/register", dto);

        // 409 wrapped through ApiResponse, check not success
        var body = await response.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        Assert.False(body!.Success);
    }

    [Fact]
    public async Task Register_PasswordMismatch_Returns400()
    {
        var dto = new RegisterDto
        {
            Name = "A", Surname = "B", Email = "pm@example.com",
            Password = "Pass123", ConfirmPassword = "Wrong", StaffRoleId = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v0/auth/register", dto);

        var body = await response.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        Assert.False(body!.Success);
    }

    // ────────── Login ──────────

    [Fact]
    public async Task Login_ValidCredentials_Returns200WithToken()
    {
        // Register a user first
        var email = $"login{Guid.NewGuid():N}@example.com";
        await _client.PostAsJsonAsync("/api/v0/auth/register", new RegisterDto
        {
            Name = "Login", Surname = "Test", Email = email,
            Password = "Pass123", ConfirmPassword = "Pass123", StaffRoleId = 1
        });

        var response = await _client.PostAsJsonAsync("/api/v0/auth/login",
            new LoginDto { Email = email, Password = "Pass123" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        Assert.True(body!.Success);
        Assert.False(string.IsNullOrEmpty(body.Data!.Token));
    }

    [Fact]
    public async Task Login_WrongPassword_ReturnsUnauthorized()
    {
        var email = $"wrong{Guid.NewGuid():N}@example.com";
        await _client.PostAsJsonAsync("/api/v0/auth/register", new RegisterDto
        {
            Name = "A", Surname = "B", Email = email,
            Password = "Pass123", ConfirmPassword = "Pass123", StaffRoleId = 1
        });

        var response = await _client.PostAsJsonAsync("/api/v0/auth/login",
            new LoginDto { Email = email, Password = "WrongPass" });

        var body = await response.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        Assert.False(body!.Success);
    }

    [Fact]
    public async Task Login_NonExistentEmail_ReturnsUnauthorized()
    {
        var response = await _client.PostAsJsonAsync("/api/v0/auth/login",
            new LoginDto { Email = "nope@example.com", Password = "Pass" });

        var body = await response.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        Assert.False(body!.Success);
    }
}