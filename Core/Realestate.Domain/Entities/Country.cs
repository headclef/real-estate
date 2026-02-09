using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities;

public class Country : BaseEntity
{
    public string? Name { get; set; }
    public string? IsoTwo { get; set; }
    public string? IsoThree { get; set; }
    public string? IsoNumber { get; set; }
    public string? Cctld { get; set; }
    public string? Plate { get; set; }
    public string? Currency { get; set; }
}