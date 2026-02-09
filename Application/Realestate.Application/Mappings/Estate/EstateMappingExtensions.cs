using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class EstateMappingExtensions
{
    public static EstateDto ToDto(this Estate src)
    {
        if (src == null) return null!;
        return new EstateDto
        {
            Id = src.Id,
            Name = src.Name,
            Description = src.Description,
            Price = src.Price,
            DivisionId = src.DivisionId,
            EstateTypeId = src.EstateTypeId,
            EstateStatusId = src.EstateStatusId
        };
    }

    public static Estate ToEntity(this CreateEstateDto src)
    {
        if (src == null) return null!;
        return new Estate
        {
            Name = src.Name,
            Description = src.Description,
            Price = src.Price,
            DivisionId = src.DivisionId,
            EstateTypeId = src.EstateTypeId,
            EstateStatusId = src.EstateStatusId
        };
    }

    public static void UpdateFrom(this Estate target, UpdateEstateDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
        target.Description = src.Description;
        target.Price = src.Price;
        target.DivisionId = src.DivisionId;
        target.EstateTypeId = src.EstateTypeId;
        target.EstateStatusId = src.EstateStatusId;
    }
}