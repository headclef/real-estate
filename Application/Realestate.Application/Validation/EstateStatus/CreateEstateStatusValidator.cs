using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.EstateStatus;

public class CreateEstateStatusValidator
{
    public Response<CreateEstateStatusDto> Validate(CreateEstateStatusDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<CreateEstateStatusDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (errors.Any())
            return Response.Fail<CreateEstateStatusDto>(errors);

        return Response.Ok(dto);
    }
}