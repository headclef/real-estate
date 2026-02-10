using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Realestate.Application.DTOs.Auth;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.Auth;
using Realestate.Application.Validation.Auth;
using Realestate.Application.Wrappers;
using Realestate.Domain.Entities.Identity;
namespace Realestate.Business.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<AuthService> _logger;
    private readonly LoginValidator _loginValidator = new();
    private readonly RegisterValidator _registerValidator = new();

    public AuthService(IUnitOfWork unitOfWork, IOptions<JwtSettings> jwtSettings, ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
    }

    public async Task<Response<TokenResponseDto>> LoginAsync(LoginDto dto)
    {
        var validation = _loginValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<TokenResponseDto>(validation.Errors!, statusCode: 400);

        var staff = await _unitOfWork.Staffs.GetByEmailAsync(dto.Email);
        if (staff == null || string.IsNullOrEmpty(staff.PasswordHash))
        {
            _logger.LogWarning("Login failed — email {Email} not found or no password set", dto.Email);
            return Response.Fail<TokenResponseDto>("Invalid email or password.", statusCode: 401);
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, staff.PasswordHash))
        {
            _logger.LogWarning("Login failed — incorrect password for email {Email}", dto.Email);
            return Response.Fail<TokenResponseDto>("Invalid email or password.", statusCode: 401);
        }

        // Load role name for token claims
        var role = await _unitOfWork.StaffRoles.GetByIdAsync(staff.StaffRoleId);
        var roleName = role?.Name ?? "Staff";

        var token = GenerateJwtToken(staff, roleName);
        _logger.LogInformation("Staff {StaffId} ({Email}) logged in successfully", staff.Id, staff.Email);

        return Response.Ok(token, "Login successful.");
    }

    public async Task<Response<TokenResponseDto>> RegisterAsync(RegisterDto dto)
    {
        var validation = _registerValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<TokenResponseDto>(validation.Errors!, statusCode: 400);

        // Check duplicate email
        var existing = await _unitOfWork.Staffs.GetByEmailAsync(dto.Email);
        if (existing != null)
        {
            _logger.LogWarning("Registration failed — email {Email} already exists", dto.Email);
            return Response.Fail<TokenResponseDto>("A user with this email already exists.", statusCode: 409);
        }

        var staff = new Staff
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Code = dto.Code,
            PhoneNumber = dto.PhoneNumber,
            StaffRoleId = dto.StaffRoleId
        };

        await _unitOfWork.Staffs.AddAsync(staff);
        await _unitOfWork.SaveChangesAsync();

        var role = await _unitOfWork.StaffRoles.GetByIdAsync(staff.StaffRoleId);
        var roleName = role?.Name ?? "Staff";

        var token = GenerateJwtToken(staff, roleName);
        _logger.LogInformation("Staff {StaffId} ({Email}) registered successfully", staff.Id, staff.Email);

        return Response.Ok(token, "Registration successful.");
    }

    // ────── Token Generation ──────

    private TokenResponseDto GenerateJwtToken(Staff staff, string roleName)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, staff.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, staff.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, staff.FullName ?? string.Empty),
            new Claim(ClaimTypes.Role, roleName)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new TokenResponseDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            StaffId = staff.Id,
            FullName = staff.FullName,
            Email = staff.Email,
            Role = roleName
        };
    }
}