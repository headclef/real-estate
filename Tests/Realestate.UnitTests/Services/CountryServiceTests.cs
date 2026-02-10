using Microsoft.Extensions.Logging;
using Moq;
using Realestate.Application.DTOs.Country;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Repositories.Country;
using Realestate.Business.Services;
using Realestate.Domain.Entities.World;
namespace Realestate.UnitTests.Services;

public class CountryServiceTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<ICountryRepository> _countryRepo = new();
    private readonly Mock<ILogger<CountryService>> _logger = new();
    private readonly CountryService _sut;

    public CountryServiceTests()
    {
        _uow.Setup(u => u.Countries).Returns(_countryRepo.Object);
        _sut = new CountryService(_uow.Object, _logger.Object);
    }

    // ────────── GetByIdAsync ──────────

    [Fact]
    public async Task GetById_ExistingId_ReturnsSuccess()
    {
        var entity = new Country { Id = 1, Name = "Turkey", IsoTwo = "TR" };
        _countryRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        var result = await _sut.GetByIdAsync(1);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.Equal("Turkey", result.Data!.Name);
        Assert.Equal("TR", result.Data.IsoTwo);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsFail()
    {
        _countryRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Country?)null);

        var result = await _sut.GetByIdAsync(999);

        Assert.False(result.Success);
        Assert.Contains(result.Errors!, e => e.Contains("not found"));
    }

    // ────────── ListAsync ──────────

    [Fact]
    public async Task List_ReturnsPagedData()
    {
        var entities = Enumerable.Range(1, 5).Select(i => new Country { Id = i, Name = $"Country{i}" });
        _countryRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var result = await _sut.ListAsync(1, 3);

        Assert.True(result.Success);
        Assert.Equal(3, result.Data!.Count());
        Assert.Equal(5, result.Total);
    }

    [Fact]
    public async Task List_Page2_ReturnsRemainingItems()
    {
        var entities = Enumerable.Range(1, 5).Select(i => new Country { Id = i, Name = $"Country{i}" });
        _countryRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var result = await _sut.ListAsync(2, 3);

        Assert.True(result.Success);
        Assert.Equal(2, result.Data!.Count()); // 5 - 3 = 2
    }

    // ────────── CreateAsync ──────────

    [Fact]
    public async Task Create_ValidDto_ReturnsSuccess()
    {
        var dto = new CreateCountryDto { Name = "Germany", IsoTwo = "DE" };
        _countryRepo.Setup(r => r.AddAsync(It.IsAny<Country>())).ReturnsAsync(1);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _sut.CreateAsync(dto);

        Assert.True(result.Success);
        Assert.Equal("Germany", result.Data!.Name);
        _countryRepo.Verify(r => r.AddAsync(It.IsAny<Country>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Create_EmptyName_FailsValidation()
    {
        var dto = new CreateCountryDto { Name = "" };

        var result = await _sut.CreateAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
        _countryRepo.Verify(r => r.AddAsync(It.IsAny<Country>()), Times.Never);
    }

    // ────────── UpdateAsync ──────────

    [Fact]
    public async Task Update_ExistingEntity_ReturnsSuccess()
    {
        var existing = new Country { Id = 1, Name = "Turkey", IsoTwo = "TR" };
        _countryRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var dto = new UpdateCountryDto { Name = "Türkiye" };
        var result = await _sut.UpdateAsync(1, dto);

        Assert.True(result.Success);
        Assert.Equal("Türkiye", result.Data!.Name);
        _countryRepo.Verify(r => r.UpdateAsync(It.IsAny<Country>()), Times.Once);
    }

    [Fact]
    public async Task Update_NonExistingEntity_ReturnsFail()
    {
        _countryRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Country?)null);

        var result = await _sut.UpdateAsync(999, new UpdateCountryDto { Name = "X" });

        Assert.False(result.Success);
    }

    // ────────── DeleteAsync ──────────

    [Fact]
    public async Task Delete_ExistingEntity_ReturnsSuccess()
    {
        var existing = new Country { Id = 1, Name = "Turkey" };
        _countryRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _sut.DeleteAsync(1);

        Assert.True(result.Success);
        Assert.True(result.Data);
        _countryRepo.Verify(r => r.DeleteAsync(It.IsAny<Country>()), Times.Once);
    }

    [Fact]
    public async Task Delete_NonExistingEntity_ReturnsFail()
    {
        _countryRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Country?)null);

        var result = await _sut.DeleteAsync(999);

        Assert.False(result.Success);
    }
}