namespace Realestate.Application.DTOs;

public class UpdateDivisionDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int CountryId { get; set; }
    public int DivisionTypeId { get; set; }
    public int? ParentId { get; set; }
}