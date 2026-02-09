namespace Realestate.Application.DTOs;

public class StaffDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? FullName { get; set; }
    public string? Code { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public int StaffRoleId { get; set; }
}