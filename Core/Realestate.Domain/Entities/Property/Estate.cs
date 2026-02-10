using Realestate.Domain.Entities.Commons;
using Realestate.Domain.Entities.World;
namespace Realestate.Domain.Entities.Property;

/// <summary>
/// Schema      : Property
/// Table       : Estate
/// Description : This entity represents a real estate property in the system. It contains details about the property such as its name, description, price, and its relationships to other entities like Division, EstateType, and EstateStatus. This entity is central to the real estate domain as it encapsulates the key information about properties that users can search for, view, and manage within the application.
/// 
/// Properties:
/// - Name: The name of the estate.
/// - Description: A detailed description of the estate.
/// - Price: The price of the estate.
/// - DivisionId: The foreign key linking to the Division entity, representing the geographical or administrative division where the estate is located.
/// - EstateTypeId: The foreign key linking to the EstateType entity, representing the type of the estate (e.g., apartment, house, commercial).
/// - EstateStatusId: The foreign key linking to the EstateStatus entity, representing the current status of the estate (e.g., available, sold, under contract).
/// 
/// Navigation Properties:
/// - Division: The related Division entity that provides additional information about the location of the estate.
/// - EstateType: The related EstateType entity that provides additional information about the type of the estate.
/// - EstateStatus: The related EstateStatus entity that provides additional information about the current status of the estate.
/// </summary>
public class Estate : BaseEntity
{
    // Properties
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }

    // Foreign keys
    public int DivisionId { get; set; }
    public int EstateTypeId { get; set; }
    public int EstateStatusId { get; set; }

    // Navigation properties
    public Division? Division { get; set; }
    public EstateType? EstateType { get; set; }
    public EstateStatus? EstateStatus { get; set; }
}