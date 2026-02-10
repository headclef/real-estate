using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.EstateStatus;

public class UpdateEstateStatusValidator
{
    public Response<UpdateEstateStatusDto> Validate(UpdateEstateStatusDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<UpdateEstateStatusDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (errors.Any())
            return Response.Fail<UpdateEstateStatusDto>(errors);

        return Response.Ok(dto);
    }
}