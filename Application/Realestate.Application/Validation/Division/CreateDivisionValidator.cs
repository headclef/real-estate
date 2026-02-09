using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Division;

public class CreateDivisionValidator
{
    public Response<CreateDivisionDto> Validate(CreateDivisionDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<CreateDivisionDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (dto.CountryId <= 0)
            errors.Add("CountryId must be a positive integer.");

        if (dto.DivisionTypeId <= 0)
            errors.Add("DivisionTypeId must be a positive integer.");

        if (errors.Any())
            return Response.Fail<CreateDivisionDto>(errors);

        return Response.Ok(dto);
    }
}