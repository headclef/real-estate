using System.Collections;
using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.World;

/// <summary>
/// Schema      :   World
/// Table       :   Country
/// Description :   This table contains the countries of the world.
/// 
/// Properties: 
/// - Name (string, nullable): The name of the country.
/// - IsoTwo (string, nullable): The ISO 3166-1 alpha-2 code of the country.
/// - IsoThree (string, nullable): The ISO 3166-1 alpha-3 code of the country.
/// - IsoNumber (string, nullable): The ISO 3166-1 numeric code of the country.
/// - Cctld (string, nullable): The country code top-level domain (ccTLD) of the country.
/// - Plate (string, nullable): The international vehicle registration code of the country.
/// - Currency (string, nullable): The currency of the country.
/// </summary>
public class Country : BaseEntity
{
    // Properties
    public string? Name { get; set; }
    public string? IsoTwo { get; set; }
    public string? IsoThree { get; set; }
    public string? IsoNumber { get; set; }
    public string? Cctld { get; set; }
    public string? Plate { get; set; }
    public string? Currency { get; set; }

    // Navigation Properties
    public ICollection<Division> Divisions { get; set; } = new HashSet<Division>();
}