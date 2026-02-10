using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.Estate;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Estate;

public class UpdateEstateValidator
{
    public Response<UpdateEstateDto> Validate(UpdateEstateDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<UpdateEstateDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (dto.Price < 0)
            errors.Add("Price must be non-negative.");

        if (dto.DivisionId <= 0)
            errors.Add("DivisionId must be a positive integer.");

        if (dto.EstateTypeId <= 0)
            errors.Add("EstateTypeId must be a positive integer.");

        if (dto.EstateStatusId <= 0)
            errors.Add("EstateStatusId must be a positive integer.");

        if (errors.Any())
            return Response.Fail<UpdateEstateDto>(errors);

        return Response.Ok(dto);
    }
}