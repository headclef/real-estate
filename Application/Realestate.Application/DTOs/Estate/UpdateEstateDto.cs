namespace Realestate.Application.DTOs.Estate;

public class UpdateEstateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DivisionId { get; set; }
    public int EstateTypeId { get; set; }
    public int EstateStatusId { get; set; }
}