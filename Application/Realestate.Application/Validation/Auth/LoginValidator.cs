using Realestate.Application.DTOs.Auth;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Auth;

public class LoginValidator
{
    public Response Validate(LoginDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");

        return errors.Count > 0
            ? Response.Fail(errors, statusCode: 400)
            : Response.Ok();
    }
}