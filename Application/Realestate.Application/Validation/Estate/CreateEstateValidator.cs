using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.Estate;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Estate;

public class CreateEstateValidator
{
    public Response<CreateEstateDto> Validate(CreateEstateDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<CreateEstateDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (dto.Price < 0)
            errors.Add("Price must be non-negative.");

        if (dto.DivisionId <= 0)
            errors.Add("DivisionId must be a positive integer.");

        if (errors.Any())
            return Response.Fail<CreateEstateDto>(errors);

        return Response.Ok(dto);
    }
}