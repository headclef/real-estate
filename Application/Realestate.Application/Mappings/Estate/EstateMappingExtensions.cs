using Realestate.Domain.Entities.Property;
using Realestate.Application.DTOs.Estate;
namespace Realestate.Application.Mappings.Estate;

public static class EstateMappingExtensions
{
    public static EstateDto ToDto(this Realestate.Domain.Entities.Property.Estate src)
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

    public static Realestate.Domain.Entities.Property.Estate ToEntity(this CreateEstateDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.Property.Estate
        {
            Name = src.Name,
            Description = src.Description,
            Price = src.Price,
            DivisionId = src.DivisionId,
            EstateTypeId = src.EstateTypeId,
            EstateStatusId = src.EstateStatusId
        };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.Property.Estate target, UpdateEstateDto src)
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