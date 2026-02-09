namespace Realestate.Application.DTOs.Division;

public class DivisionDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int CountryId { get; set; }
    public int DivisionTypeId { get; set; }
    public int? ParentId { get; set; }
}