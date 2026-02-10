using Realestate.Application.DTOs.Auth;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.Auth;

public interface IAuthService
{
    Task<Response<TokenResponseDto>> LoginAsync(LoginDto dto);
    Task<Response<TokenResponseDto>> RegisterAsync(RegisterDto dto);
}