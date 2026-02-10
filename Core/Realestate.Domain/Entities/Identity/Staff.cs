using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities.Identity;

/// <summary>
/// Schema      : Identity
/// Table       : Staff
/// Description : Staff members of the real estate agency, including their roles and contact information.
/// 
/// Properties:
/// - Name: The first name of the staff member.
/// - Surname: The last name of the staff member.
/// - FullName: A computed property that combines Name and Surname for easy display.
/// - Code: A unique code assigned to each staff member for identification purposes.
/// - Email: The email address of the staff member for communication.
/// - PhoneNumber: The contact phone number of the staff member.
/// - StaffRoleId: A foreign key linking to the StaffRole entity, indicating the role of the staff member within the agency.
/// 
/// Navigation Properties:
/// - StaffRole: A reference to the StaffRole entity, providing details about the staff member's role and permissions within the agency.
/// </summary>
public class Staff : BaseEntity
{
    // Properties
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? FullName => string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Surname) ? null : $"{Name} {Surname}";
    public string? Code { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    // Foreign keys
    public int StaffRoleId { get; set; }

    // Navigation properties
    public StaffRole? StaffRole { get; set; }
}