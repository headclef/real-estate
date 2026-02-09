namespace Realestate.Application.DTOs;

public class CreateStaffDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Code { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int StaffRoleId { get; set; }
}