using System.Collections.Generic;
using System.Linq;
using Realestate.Application.DTOs.Division;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Validation.Division;

public class UpdateDivisionValidator
{
    public Response<UpdateDivisionDto> Validate(UpdateDivisionDto dto)
    {
        var errors = new List<string>();
        if (dto == null)
        {
            errors.Add("Payload is required.");
            return Response.Fail<UpdateDivisionDto>(errors);
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors.Add("Name is required.");

        if (dto.CountryId <= 0)
            errors.Add("CountryId must be a positive integer.");

        if (dto.DivisionTypeId <= 0)
            errors.Add("DivisionTypeId must be a positive integer.");

        if (errors.Any())
            return Response.Fail<UpdateDivisionDto>(errors);

        return Response.Ok(dto);
    }
}