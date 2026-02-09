using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Staff;

public class CreateStaffValidator
{
    public Response<CreateStaffDto> Validate(CreateStaffDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<CreateStaffDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name) && string.IsNullOrWhiteSpace(dto.Surname))
            errors.Add("At least one of Name or Surname is required.");

        if (dto.StaffRoleId <= 0)
            errors.Add("StaffRoleId must be a positive integer.");

        if (errors.Any())
            return Response.Fail<CreateStaffDto>(errors);

        return Response.Ok(dto);
    }
}