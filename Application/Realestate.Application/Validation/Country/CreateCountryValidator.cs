using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Country;

public class CreateCountryValidator
{
    public Response<CreateCountryDto> Validate(CreateCountryDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<CreateCountryDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (errors.Any())
            return Response.Fail<CreateCountryDto>(errors);

        return Response.Ok(dto);
    }
}