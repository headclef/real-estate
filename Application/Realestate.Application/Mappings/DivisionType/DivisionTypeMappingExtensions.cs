using Realestate.Domain.Entities;
using Realestate.Application.DTOs.DivisionType;
namespace Realestate.Application.Mappings.DivisionType;

public static class DivisionTypeMappingExtensions
{
    public static DivisionTypeDto ToDto(this Realestate.Domain.Entities.DivisionType src)
    {
        if (src == null) return null!;
        return new DivisionTypeDto { Id = src.Id, Name = src.Name };
    }

    public static Realestate.Domain.Entities.DivisionType ToEntity(this CreateDivisionTypeDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.DivisionType { Name = src.Name };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.DivisionType target, UpdateDivisionTypeDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}