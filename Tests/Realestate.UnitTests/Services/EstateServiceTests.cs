using Microsoft.Extensions.Logging;
using Moq;
using Realestate.Application.DTOs.Estate;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Repositories.Estate;
using Realestate.Business.Services;
using Realestate.Domain.Entities.Property;
namespace Realestate.UnitTests.Services;

public class EstateServiceTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IEstateRepository> _estateRepo = new();
    private readonly Mock<ILogger<EstateService>> _logger = new();
    private readonly EstateService _sut;

    public EstateServiceTests()
    {
        _uow.Setup(u => u.Estates).Returns(_estateRepo.Object);
        _sut = new EstateService(_uow.Object, _logger.Object);
    }

    // ────────── GetByIdAsync ──────────

    [Fact]
    public async Task GetById_ExistingId_ReturnsEstate()
    {
        var entity = new Estate { Id = 1, Name = "Beach Villa", Price = 250_000m, DivisionId = 5, EstateTypeId = 1, EstateStatusId = 1 };
        _estateRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        var result = await _sut.GetByIdAsync(1);

        Assert.True(result.Success);
        Assert.Equal("Beach Villa", result.Data!.Name);
        Assert.Equal(250_000m, result.Data.Price);
    }

    [Fact]
    public async Task GetById_NonExisting_ReturnsFail()
    {
        _estateRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Estate?)null);

        var result = await _sut.GetByIdAsync(999);

        Assert.False(result.Success);
    }

    // ────────── ListAsync ──────────

    [Fact]
    public async Task List_ReturnsPagedEstates()
    {
        var entities = Enumerable.Range(1, 10).Select(i => new Estate { Id = i, Name = $"Estate{i}", Price = i * 100_000m, DivisionId = 1, EstateTypeId = 1, EstateStatusId = 1 });
        _estateRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var result = await _sut.ListAsync(1, 5);

        Assert.True(result.Success);
        Assert.Equal(5, result.Data!.Count());
        Assert.Equal(10, result.Total);
    }

    // ────────── CreateAsync ──────────

    [Fact]
    public async Task Create_ValidDto_ReturnsSuccess()
    {
        var dto = new CreateEstateDto { Name = "Mountain House", Price = 300_000m, DivisionId = 2, EstateTypeId = 1, EstateStatusId = 1 };
        _estateRepo.Setup(r => r.AddAsync(It.IsAny<Estate>())).ReturnsAsync(1);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _sut.CreateAsync(dto);

        Assert.True(result.Success);
        Assert.Equal("Mountain House", result.Data!.Name);
        _estateRepo.Verify(r => r.AddAsync(It.IsAny<Estate>()), Times.Once);
    }

    [Fact]
    public async Task Create_EmptyName_FailsValidation()
    {
        var dto = new CreateEstateDto { Name = "", Price = 100, DivisionId = 1, EstateTypeId = 1, EstateStatusId = 1 };

        var result = await _sut.CreateAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
        _estateRepo.Verify(r => r.AddAsync(It.IsAny<Estate>()), Times.Never);
    }

    [Fact]
    public async Task Create_NegativePrice_FailsValidation()
    {
        var dto = new CreateEstateDto { Name = "Test", Price = -1, DivisionId = 1, EstateTypeId = 1, EstateStatusId = 1 };

        var result = await _sut.CreateAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    // ────────── UpdateAsync ──────────

    [Fact]
    public async Task Update_ExistingEstate_ReturnsSuccess()
    {
        var existing = new Estate { Id = 1, Name = "Old", Price = 100_000m, DivisionId = 1, EstateTypeId = 1, EstateStatusId = 1 };
        _estateRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var dto = new UpdateEstateDto { Name = "Renamed Estate", Price = 200_000m, DivisionId = 1, EstateTypeId = 1, EstateStatusId = 1 };
        var result = await _sut.UpdateAsync(1, dto);

        Assert.True(result.Success);
        Assert.Equal("Renamed Estate", result.Data!.Name);
    }

    [Fact]
    public async Task Update_NonExisting_ReturnsFail()
    {
        _estateRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Estate?)null);

        var result = await _sut.UpdateAsync(999, new UpdateEstateDto { Name = "X" });

        Assert.False(result.Success);
    }

    // ────────── DeleteAsync ──────────

    [Fact]
    public async Task Delete_ExistingEstate_ReturnsTrue()
    {
        var existing = new Estate { Id = 1, Name = "Test" };
        _estateRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _sut.DeleteAsync(1);

        Assert.True(result.Success);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task Delete_NonExisting_ReturnsFail()
    {
        _estateRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Estate?)null);

        var result = await _sut.DeleteAsync(999);

        Assert.False(result.Success);
    }
}