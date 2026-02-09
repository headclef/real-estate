using Realestate.Domain.Entities;
using Realestate.Application.DTOs.Country;
namespace Realestate.Application.Mappings.Country;

public static class CountryMappingExtensions
{
    public static CountryDto ToDto(this Realestate.Domain.Entities.Country src)
    {
        if (src == null) return null!;
        return new CountryDto
        {
            Id = src.Id,
            Name = src.Name,
            IsoTwo = src.IsoTwo,
            IsoThree = src.IsoThree,
            IsoNumber = src.IsoNumber,
            Cctld = src.Cctld,
            Plate = src.Plate,
            Currency = src.Currency
        };
    }

    public static Realestate.Domain.Entities.Country ToEntity(this CreateCountryDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.Country
        {
            Name = src.Name,
            IsoTwo = src.IsoTwo,
            IsoThree = src.IsoThree,
            IsoNumber = src.IsoNumber,
            Cctld = src.Cctld,
            Plate = src.Plate,
            Currency = src.Currency
        };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.Country target, UpdateCountryDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
        target.IsoTwo = src.IsoTwo;
        target.IsoThree = src.IsoThree;
        target.IsoNumber = src.IsoNumber;
        target.Cctld = src.Cctld;
        target.Plate = src.Plate;
        target.Currency = src.Currency;
    }
}