using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Auth;
using Realestate.Application.Interfaces.Services.Auth;
namespace Realestate.API.Controllers;

/// <summary>
/// Authentication endpoints — login &amp; registration.
/// </summary>
[Tags("Auth")]
public class AuthController : ApiBaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Authenticate with email and password to receive a JWT token.</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return ApiResponse(result);
    }

    /// <summary>Register a new staff member and receive a JWT token.</summary>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        return ApiResponse(result);
    }
}