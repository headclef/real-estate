using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities;

public class Staff : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? FullName => string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Surname) ? null : $"{Name} {Surname}";
    public string? Code { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int StaffRoleId { get; set; }

    // Navigation properties
    public StaffRole? StaffRole { get; set; }
}