using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class CountryMappingExtensions
{
    public static CountryDto ToDto(this Country src)
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

    public static Country ToEntity(this CreateCountryDto src)
    {
        if (src == null) return null!;
        return new Country
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

    public static void UpdateFrom(this Country target, UpdateCountryDto src)
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