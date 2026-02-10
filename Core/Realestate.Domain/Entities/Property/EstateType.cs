using System.Collections;
using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.Property;

/// <summary>
/// Schema      : Property
/// Table       : EstateType
/// Description : This entity defines the various types of estates available in the real estate domain. It can be used to categorize properties based on their usage or characteristics. For example, it can include types like "Residential", "Commercial", "Industrial", etc. This helps in organizing and filtering properties based on their type when performing operations such as searching, listing, or managing properties in the system.
/// 
/// Properties:
/// - Name: The name of the estate type (e.g., "Residential", "Commercial").
/// 
/// Navigation Properties:
/// - Estates: A collection of estates that belong to this estate type. This represents the relationship between the estate type and the estates that are categorized under it.
/// </summary>
public class EstateType : BaseEntity
{
    // Properties
    public string? Name { get; set; }

    // Navigation Properties
    public ICollection<Estate> Estates { get; set; } = new HashSet<Estate>();
}