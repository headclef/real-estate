using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class DivisionTypeMappingExtensions
{
    public static DivisionTypeDto ToDto(this DivisionType src)
    {
        if (src == null) return null!;
        return new DivisionTypeDto { Id = src.Id, Name = src.Name };
    }

    public static DivisionType ToEntity(this CreateDivisionTypeDto src)
    {
        if (src == null) return null!;
        return new DivisionType { Name = src.Name };
    }

    public static void UpdateFrom(this DivisionType target, UpdateDivisionTypeDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}