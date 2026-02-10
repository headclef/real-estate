using System.Collections;
using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.Identity;

/// <summary>
/// Schema      : Identity
/// Table       : StaffRole
/// Description : This table is used to store the roles of staff members in the real estate application. Each role can have specific permissions and responsibilities associated with it, allowing for better management of staff members and their access to various features and functionalities within the application.
/// 
/// Properties:
/// - Name: The name of the staff role (e.g., "Manager", "Agent", "Administrator"). This property is nullable, allowing for flexibility in cases where a role may not have a specific name assigned.
/// 
/// Navigation Properties:
/// - Staffs: A collection of staff members that are associated with this role. This represents the relationship between the staff role and the staff members who hold that role, allowing for easy retrieval of staff members based on their assigned roles in the system.
/// </summary>
public class StaffRole : BaseEntity
{
    // Properties
    public string? Name { get; set; }

    // Navigation Properties
    public ICollection<Staff> Staffs { get; set; } = new HashSet<Staff>();
}