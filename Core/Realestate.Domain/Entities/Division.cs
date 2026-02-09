using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities;

public class Division : BaseEntity
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int CountryId { get; set; }
    public int DivisionTypeId { get; set; }
    public int? ParentId { get; set; }

    // Navigation properties
    public Country? Country { get; set; }
    public DivisionType? DivisionType { get; set; }
    public Division? ParentDivision { get; set; }
}