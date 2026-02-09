using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.DivisionType;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.DivisionType;

public class CreateDivisionTypeValidator
{
    public Response<CreateDivisionTypeDto> Validate(CreateDivisionTypeDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<CreateDivisionTypeDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (errors.Any())
            return Response.Fail<CreateDivisionTypeDto>(errors);

        return Response.Ok(dto);
    }
}