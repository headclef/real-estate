using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.World;

/// <summary>
/// Schema      :   World
/// Table       :   Division
/// Description :   This table contains the divisions of a country, such as states, provinces, etc.
/// 
/// Properties: 
/// - Name (string, nullable): The name of the division.
/// - Code (string, nullable): The code of the division.
/// - CountryId (int, not nullable): The foreign key to the Country table.
/// - DivisionTypeId (int, not nullable): The foreign key to the DivisionType table.
/// - ParentId (int, nullable): The foreign key to the parent division, if applicable
/// 
/// Navigation Properties:
/// - Country: The country to which the division belongs.
/// - DivisionType: The type of the division (e.g., state, province).
/// - ParentDivision: The parent division, if applicable (e.g., a state may have a parent division of "United States").
/// </summary>
public class Division : BaseEntity
{
    // Properties
    public string? Name { get; set; }
    public string? Code { get; set; }

    // Foreign keys
    public int CountryId { get; set; }
    public int DivisionTypeId { get; set; }
    public int? ParentId { get; set; }

    // Navigation properties
    public Country? Country { get; set; }
    public DivisionType? DivisionType { get; set; }
    public Division? ParentDivision { get; set; }
}