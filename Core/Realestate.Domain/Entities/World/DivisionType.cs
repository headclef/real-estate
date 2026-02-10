using System.Collections;
using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.World;

/// <summary>
/// Schema      :   World
/// Table       :   DivisionType
/// Description :   This table contains the types of divisions, such as state, province, etc.
/// 
/// Properties:
/// - Name (string, nullable): The name of the division type.
/// 
/// Navigation Properties:
/// - Divisions: The divisions that belong to this division type (e.g., all states belong to the "State" division type).
/// </summary>
public class DivisionType : BaseEntity
{
    // Properties
    public string? Name { get; set; }

    // Navigation properties
    public ICollection<Division>? Divisions { get; set; } = new HashSet<Division>();
}