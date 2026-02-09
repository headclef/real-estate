namespace Realestate.Application.DTOs.Country;

public class CreateCountryDto
{
    public string? Name { get; set; }
    public string? IsoTwo { get; set; }
    public string? IsoThree { get; set; }
    public string? IsoNumber { get; set; }
    public string? Cctld { get; set; }
    public string? Plate { get; set; }
    public string? Currency { get; set; }
}