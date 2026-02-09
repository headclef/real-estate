using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class EstateStatusMappingExtensions
{
    public static EstateStatusDto ToDto(this EstateStatus src)
    {
        if (src == null) return null!;
        return new EstateStatusDto { Id = src.Id, Name = src.Name };
    }

    public static EstateStatus ToEntity(this CreateEstateStatusDto src)
    {
        if (src == null) return null!;
        return new EstateStatus { Name = src.Name };
    }

    public static void UpdateFrom(this EstateStatus target, UpdateEstateStatusDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}