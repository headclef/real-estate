using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.StaffRole;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.StaffRole;

public class UpdateStaffRoleValidator
{
    public Response<UpdateStaffRoleDto> Validate(UpdateStaffRoleDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<UpdateStaffRoleDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (errors.Any())
            return Response.Fail<UpdateStaffRoleDto>(errors);

        return Response.Ok(dto);
    }
}