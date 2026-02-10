using Realestate.Domain.Entities.World;
using Realestate.Application.DTOs.Division;
namespace Realestate.Application.Mappings.Division;

public static class DivisionMappingExtensions
{
    public static DivisionDto ToDto(this Realestate.Domain.Entities.World.Division src)
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

    public static Realestate.Domain.Entities.World.Division ToEntity(this CreateDivisionDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.World.Division
        {
            Name = src.Name,
            Code = src.Code,
            CountryId = src.CountryId,
            DivisionTypeId = src.DivisionTypeId,
            ParentId = src.ParentId
        };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.World.Division target, UpdateDivisionDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
        target.Code = src.Code;
        target.CountryId = src.CountryId;
        target.DivisionTypeId = src.DivisionTypeId;
        target.ParentId = src.ParentId;
    }
}