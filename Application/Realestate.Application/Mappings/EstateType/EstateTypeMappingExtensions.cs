using Realestate.Domain.Entities;
using Realestate.Application.DTOs.EstateType;
namespace Realestate.Application.Mappings.EstateType;

public static class EstateTypeMappingExtensions
{
    public static EstateTypeDto ToDto(this Realestate.Domain.Entities.EstateType src)
    {
        if (src == null) return null!;
        return new EstateTypeDto { Id = src.Id, Name = src.Name };
    }

    public static Realestate.Domain.Entities.EstateType ToEntity(this CreateEstateTypeDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.EstateType { Name = src.Name };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.EstateType target, UpdateEstateTypeDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}