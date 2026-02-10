using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Realestate.Application.DTOs.Auth;
using Realestate.Application.DTOs.Country;
using Realestate.Application.Wrappers;
namespace Realestate.IntegrationTests.Controllers;

public class CrudControllerTests : IClassFixture<RealEstateWebAppFactory>
{
    private readonly HttpClient _client;

    public CrudControllerTests(RealEstateWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<string> GetTokenAsync()
    {
        var email = $"crud{Guid.NewGuid():N}@example.com";
        var reg = await _client.PostAsJsonAsync("/api/v0/auth/register", new RegisterDto
        {
            Name = "Crud", Surname = "Tester", Email = email,
            Password = "Pass123", ConfirmPassword = "Pass123", StaffRoleId = 1
        });
        var body = await reg.Content.ReadFromJsonAsync<Response<TokenResponseDto>>();
        return body!.Data!.Token;
    }

    private async Task<HttpClient> AuthenticatedClientAsync()
    {
        var token = await GetTokenAsync();
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return _client;
    }

    // ────────── Unauthorized Access ──────────

    [Fact]
    public async Task Country_GetAll_WithoutToken_Returns401()
    {
        var client = new RealEstateWebAppFactory().CreateClient();
        var response = await client.GetAsync("/api/v0/country");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // ────────── Country CRUD ──────────

    [Fact]
    public async Task Country_FullCrudCycle_Works()
    {
        var client = await AuthenticatedClientAsync();

        // Create
        var createDto = new CreateCountryDto { Name = "TestLand", IsoTwo = "TL" };
        var createResp = await client.PostAsJsonAsync("/api/v0/country", createDto);
        Assert.True(createResp.IsSuccessStatusCode);
        var created = await createResp.Content.ReadFromJsonAsync<Response<CountryDto>>();
        Assert.True(created!.Success);
        var id = created.Data!.Id;
        Assert.True(id > 0);

        // GetById
        var getResp = await client.GetAsync($"/api/v0/country/{id}");
        Assert.True(getResp.IsSuccessStatusCode);
        var fetched = await getResp.Content.ReadFromJsonAsync<Response<CountryDto>>();
        Assert.Equal("TestLand", fetched!.Data!.Name);

        // Update
        var updateDto = new UpdateCountryDto { Name = "UpdatedLand", IsoTwo = "UL" };
        var updateResp = await client.PutAsJsonAsync($"/api/v0/country/{id}", updateDto);
        Assert.True(updateResp.IsSuccessStatusCode);
        var updated = await updateResp.Content.ReadFromJsonAsync<Response<CountryDto>>();
        Assert.Equal("UpdatedLand", updated!.Data!.Name);

        // Delete (soft)
        var deleteResp = await client.DeleteAsync($"/api/v0/country/{id}");
        Assert.True(deleteResp.IsSuccessStatusCode);
        var deleted = await deleteResp.Content.ReadFromJsonAsync<Response<bool>>();
        Assert.True(deleted!.Data);

        // After soft delete, GetById should fail
        var afterDelete = await client.GetAsync($"/api/v0/country/{id}");
        var afterBody = await afterDelete.Content.ReadFromJsonAsync<Response<CountryDto>>();
        Assert.False(afterBody!.Success);
    }

    // ────────── GetAll with pagination ──────────

    [Fact]
    public async Task Country_GetAll_ReturnsList()
    {
        var client = await AuthenticatedClientAsync();

        // Create a couple
        await client.PostAsJsonAsync("/api/v0/country", new CreateCountryDto { Name = "Alpha", IsoTwo = "AA" });
        await client.PostAsJsonAsync("/api/v0/country", new CreateCountryDto { Name = "Beta", IsoTwo = "BB" });

        var response = await client.GetAsync("/api/v0/country?page=1&pageSize=50");
        Assert.True(response.IsSuccessStatusCode);
    }
}