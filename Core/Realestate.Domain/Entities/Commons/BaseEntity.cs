namespace Realestate.Domain.Entities.Commons;

/// <summary>
/// BaseEntity is an abstract class that serves as a base for all entities in the real estate domain. It provides common properties that are shared across different entities, such as Id, IsDeleted, CreatedAt, and UpdatedAt. This allows for consistent handling of these properties across the application and promotes code reusability. The Id property is used as a unique identifier for each entity, while IsDeleted indicates whether the entity has been soft-deleted. CreatedAt and UpdatedAt track the timestamps for when the entity was created and last updated, respectively.
/// 
/// Properties:
/// - Id: A unique identifier for the entity, typically used as the primary key in the database.
/// - IsDeleted: A boolean flag indicating whether the entity has been soft-deleted. This allows for logical deletion of records without physically removing them from the database, enabling features like data recovery and auditing.
/// - CreatedAt: A timestamp indicating when the entity was created. This is useful for tracking the creation time of records and can be used for auditing and historical data analysis.
/// - UpdatedAt: A nullable timestamp indicating when the entity was last updated. This allows for tracking changes to the entity over time and can be used for auditing, synchronization, and conflict resolution in scenarios where multiple users or processes may be modifying the same entity concurrently.
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}