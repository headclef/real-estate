using Realestate.Domain.Entities.Commons;
namespace Realestate.Domain.Entities;

public class Estate : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DivisionId { get; set; }
    public int EstateTypeId { get; set; }
    public int EstateStatusId { get; set; }

    // Navigation properties
    public Division? Division { get; set; }
    public EstateType? EstateType { get; set; }
    public EstateStatus? EstateStatus { get; set; }
}