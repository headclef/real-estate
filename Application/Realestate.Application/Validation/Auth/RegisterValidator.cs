using Realestate.Application.DTOs.Auth;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Auth;

public class RegisterValidator
{
    public Response Validate(RegisterDto dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (string.IsNullOrWhiteSpace(dto.Surname))
            errors.Add("Surname is required.");

        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required.");
        else if (!dto.Email.Contains('@'))
            errors.Add("Email format is invalid.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");
        else if (dto.Password.Length < 6)
            errors.Add("Password must be at least 6 characters.");

        if (dto.Password != dto.ConfirmPassword)
            errors.Add("Password and ConfirmPassword do not match.");

        if (dto.StaffRoleId <= 0)
            errors.Add("StaffRoleId must be a positive integer.");

        return errors.Count > 0
            ? Response.Fail(errors, statusCode: 400)
            : Response.Ok();
    }
}