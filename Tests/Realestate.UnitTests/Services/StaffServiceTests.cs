using Microsoft.Extensions.Logging;
using Moq;
using Realestate.Application.DTOs.Staff;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Business.Services;
using Realestate.Domain.Entities.Identity;
namespace Realestate.UnitTests.Services;

public class StaffServiceTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IStaffRepository> _staffRepo = new();
    private readonly Mock<ILogger<StaffService>> _logger = new();
    private readonly StaffService _sut;

    public StaffServiceTests()
    {
        _uow.Setup(u => u.Staffs).Returns(_staffRepo.Object);
        _sut = new StaffService(_uow.Object, _logger.Object);
    }

    // ────────── GetByIdAsync ──────────

    [Fact]
    public async Task GetById_ExistingStaff_ReturnsSuccess()
    {
        var entity = new Staff { Id = 1, Name = "Ali", Surname = "Yilmaz", Email = "ali@test.com", Code = "S001", StaffRoleId = 1 };
        _staffRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);

        var result = await _sut.GetByIdAsync(1);

        Assert.True(result.Success);
        Assert.Equal("Ali", result.Data!.Name);
        Assert.Equal("ali@test.com", result.Data.Email);
    }

    [Fact]
    public async Task GetById_NonExisting_ReturnsFail()
    {
        _staffRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Staff?)null);

        var result = await _sut.GetByIdAsync(999);

        Assert.False(result.Success);
    }

    // ────────── ListAsync ──────────

    [Fact]
    public async Task List_ReturnsPagedStaff()
    {
        var entities = Enumerable.Range(1, 8).Select(i => new Staff { Id = i, Name = $"Staff{i}", StaffRoleId = 1 });
        _staffRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var result = await _sut.ListAsync(1, 5);

        Assert.True(result.Success);
        Assert.Equal(5, result.Data!.Count());
        Assert.Equal(8, result.Total);
    }

    // ────────── CreateAsync ──────────

    [Fact]
    public async Task Create_ValidDto_ReturnsSuccess()
    {
        var dto = new CreateStaffDto { Name = "Mehmet", Surname = "K", Email = "mehmet@test.com", Code = "S002", StaffRoleId = 1 };
        _staffRepo.Setup(r => r.AddAsync(It.IsAny<Staff>())).ReturnsAsync(1);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _sut.CreateAsync(dto);

        Assert.True(result.Success);
        Assert.Equal("Mehmet", result.Data!.Name);
        _staffRepo.Verify(r => r.AddAsync(It.IsAny<Staff>()), Times.Once);
    }

    [Fact]
    public async Task Create_EmptyNameAndSurname_FailsValidation()
    {
        var dto = new CreateStaffDto { Name = "", Surname = "", Email = "test@test.com", StaffRoleId = 1 };

        var result = await _sut.CreateAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    // ────────── UpdateAsync ──────────

    [Fact]
    public async Task Update_ExistingStaff_ReturnsSuccess()
    {
        var existing = new Staff { Id = 1, Name = "Ali", Surname = "Y", Email = "ali@test.com", StaffRoleId = 1 };
        _staffRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var dto = new UpdateStaffDto { Name = "Veli", StaffRoleId = 1 };
        var result = await _sut.UpdateAsync(1, dto);

        Assert.True(result.Success);
        Assert.Equal("Veli", result.Data!.Name);
    }

    [Fact]
    public async Task Update_NonExisting_ReturnsFail()
    {
        _staffRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Staff?)null);

        var result = await _sut.UpdateAsync(999, new UpdateStaffDto { Name = "X" });

        Assert.False(result.Success);
    }

    // ────────── DeleteAsync ──────────

    [Fact]
    public async Task Delete_ExistingStaff_ReturnsTrue()
    {
        var existing = new Staff { Id = 1, Name = "Ali", StaffRoleId = 1 };
        _staffRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _sut.DeleteAsync(1);

        Assert.True(result.Success);
        Assert.True(result.Data);
    }

    [Fact]
    public async Task Delete_NonExisting_ReturnsFail()
    {
        _staffRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Staff?)null);

        var result = await _sut.DeleteAsync(999);

        Assert.False(result.Success);
    }
}