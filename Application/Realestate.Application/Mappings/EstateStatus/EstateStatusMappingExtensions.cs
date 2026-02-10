using Realestate.Domain.Entities.Property;
using Realestate.Application.DTOs.EstateStatus;
namespace Realestate.Application.Mappings.EstateStatus;

public static class EstateStatusMappingExtensions
{
    public static EstateStatusDto ToDto(this Realestate.Domain.Entities.Property.EstateStatus src)
    {
        if (src == null) return null!;
        return new EstateStatusDto { Id = src.Id, Name = src.Name };
    }

    public static Realestate.Domain.Entities.Property.EstateStatus ToEntity(this CreateEstateStatusDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.Property.EstateStatus { Name = src.Name };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.Property.EstateStatus target, UpdateEstateStatusDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}