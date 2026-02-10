using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.EstateType;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.EstateType;

public class UpdateEstateTypeValidator
{
    public Response<UpdateEstateTypeDto> Validate(UpdateEstateTypeDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<UpdateEstateTypeDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (errors.Any())
            return Response.Fail<UpdateEstateTypeDto>(errors);

        return Response.Ok(dto);
    }
}