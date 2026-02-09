using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class DivisionMappingExtensions
{
    public static DivisionDto ToDto(this Division src)
    {
        if (src == null) return null!;
        return new DivisionDto
        {
            Id = src.Id,
            Name = src.Name,
            Code = src.Code,
            CountryId = src.CountryId,
            DivisionTypeId = src.DivisionTypeId,
            ParentId = src.ParentId
        };
    }

    public static Division ToEntity(this CreateDivisionDto src)
    {
        if (src == null) return null!;
        return new Division
        {
            Name = src.Name,
            Code = src.Code,
            CountryId = src.CountryId,
            DivisionTypeId = src.DivisionTypeId,
            ParentId = src.ParentId
        };
    }

    public static void UpdateFrom(this Division target, UpdateDivisionDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
        target.Code = src.Code;
        target.CountryId = src.CountryId;
        target.DivisionTypeId = src.DivisionTypeId;
        target.ParentId = src.ParentId;
    }
}