using System.Collections;
using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.Property;

/// <summary>
/// Schema      : Property
/// Table       : EstateStatus
/// Description : This entity defines the various statuses that an estate can have in the real estate domain. It can be used to represent the current state of a property, such as "Available", "Sold", "Under Contract", etc. This helps in tracking and managing the lifecycle of properties in the system, allowing users to filter and search for properties based on their status.
/// 
/// Properties:
/// - Name: The name of the estate status (e.g., "Available", "Sold").
/// 
/// Navigation Properties:
/// - Estates: A collection of estates that have this status. This represents the relationship between the estate status and the estates that are currently in that status, allowing for easy retrieval of properties based on their current state in the real estate market.
/// </summary>
public class EstateStatus : BaseEntity
{
    // Properties
    public string? Name { get; set; }

    // Navigation Properties
    public ICollection<Estate> Estates { get; set; } = new HashSet<Estate>();
}