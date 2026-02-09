using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class EstateTypeMappingExtensions
{
    public static EstateTypeDto ToDto(this EstateType src)
    {
        if (src == null) return null!;
        return new EstateTypeDto { Id = src.Id, Name = src.Name };
    }

    public static EstateType ToEntity(this CreateEstateTypeDto src)
    {
        if (src == null) return null!;
        return new EstateType { Name = src.Name };
    }

    public static void UpdateFrom(this EstateType target, UpdateEstateTypeDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}