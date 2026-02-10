using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Realestate.Application.DTOs.Auth;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Interfaces.Repositories.StaffRole;
using Realestate.Business.Services;
using Realestate.Domain.Entities.Identity;
namespace Realestate.UnitTests.Services;

public class AuthServiceTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IStaffRepository> _staffRepo = new();
    private readonly Mock<IStaffRoleRepository> _roleRepo = new();
    private readonly Mock<ILogger<AuthService>> _logger = new();
    private readonly AuthService _sut;

    private static readonly JwtSettings TestJwtSettings = new()
    {
        Secret = "ThisIsATestSecretKeyThatIsLongEnoughForHmac256!",
        Issuer = "Test.Issuer",
        Audience = "Test.Audience",
        ExpirationInMinutes = 30
    };

    public AuthServiceTests()
    {
        _uow.Setup(u => u.Staffs).Returns(_staffRepo.Object);
        _uow.Setup(u => u.StaffRoles).Returns(_roleRepo.Object);

        var options = Options.Create(TestJwtSettings);
        _sut = new AuthService(_uow.Object, options, _logger.Object);
    }

    // ────────── Register ──────────

    [Fact]
    public async Task Register_ValidDto_ReturnsTokenAndCreatesStaff()
    {
        var dto = new RegisterDto
        {
            Name = "Ali",
            Surname = "Yilmaz",
            Email = "ali@example.com",
            Password = "Pass123",
            ConfirmPassword = "Pass123",
            StaffRoleId = 1
        };

        _staffRepo.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((Staff?)null);
        _staffRepo.Setup(r => r.AddAsync(It.IsAny<Staff>())).ReturnsAsync(1);
        _uow.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        _roleRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new StaffRole { Id = 1, Name = "Agent" });

        var result = await _sut.RegisterAsync(dto);

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.False(string.IsNullOrEmpty(result.Data!.Token));
        Assert.Equal("ali@example.com", result.Data.Email);
        Assert.Equal("Agent", result.Data.Role);
        _staffRepo.Verify(r => r.AddAsync(It.IsAny<Staff>()), Times.Once);
        _uow.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Register_DuplicateEmail_FailsWith409()
    {
        var dto = new RegisterDto
        {
            Name = "Ali", Surname = "Y", Email = "dup@example.com",
            Password = "Pass123", ConfirmPassword = "Pass123", StaffRoleId = 1
        };
        _staffRepo.Setup(r => r.GetByEmailAsync(dto.Email))
            .ReturnsAsync(new Staff { Id = 99, Email = dto.Email });

        var result = await _sut.RegisterAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(409, result.StatusCode);
    }

    [Fact]
    public async Task Register_PasswordMismatch_FailsWith400()
    {
        var dto = new RegisterDto
        {
            Name = "Ali", Surname = "Y", Email = "ali@example.com",
            Password = "Pass123", ConfirmPassword = "Wrong", StaffRoleId = 1
        };

        var result = await _sut.RegisterAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    [Fact]
    public async Task Register_EmptyEmail_FailsValidation()
    {
        var dto = new RegisterDto
        {
            Name = "Ali", Surname = "Y", Email = "",
            Password = "Pass123", ConfirmPassword = "Pass123", StaffRoleId = 1
        };

        var result = await _sut.RegisterAsync(dto);

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }

    // ────────── Login ──────────

    [Fact]
    public async Task Login_ValidCredentials_ReturnsToken()
    {
        var password = "Pass123";
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var staff = new Staff { Id = 1, Name = "Ali", Surname = "Y", Email = "ali@example.com", PasswordHash = hash, StaffRoleId = 1 };

        _staffRepo.Setup(r => r.GetByEmailAsync("ali@example.com")).ReturnsAsync(staff);
        _roleRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new StaffRole { Id = 1, Name = "Agent" });

        var result = await _sut.LoginAsync(new LoginDto { Email = "ali@example.com", Password = password });

        Assert.True(result.Success);
        Assert.NotNull(result.Data);
        Assert.False(string.IsNullOrEmpty(result.Data!.Token));
        Assert.Equal(1, result.Data.StaffId);
    }

    [Fact]
    public async Task Login_WrongPassword_FailsWith401()
    {
        var hash = BCrypt.Net.BCrypt.HashPassword("CorrectPass");
        var staff = new Staff { Id = 1, Email = "ali@example.com", PasswordHash = hash, StaffRoleId = 1 };
        _staffRepo.Setup(r => r.GetByEmailAsync("ali@example.com")).ReturnsAsync(staff);

        var result = await _sut.LoginAsync(new LoginDto { Email = "ali@example.com", Password = "WrongPass" });

        Assert.False(result.Success);
        Assert.Equal(401, result.StatusCode);
    }

    [Fact]
    public async Task Login_NonExistentEmail_FailsWith401()
    {
        _staffRepo.Setup(r => r.GetByEmailAsync("nope@example.com")).ReturnsAsync((Staff?)null);

        var result = await _sut.LoginAsync(new LoginDto { Email = "nope@example.com", Password = "any" });

        Assert.False(result.Success);
        Assert.Equal(401, result.StatusCode);
    }

    [Fact]
    public async Task Login_EmptyFields_FailsValidation()
    {
        var result = await _sut.LoginAsync(new LoginDto { Email = "", Password = "" });

        Assert.False(result.Success);
        Assert.Equal(400, result.StatusCode);
    }
}