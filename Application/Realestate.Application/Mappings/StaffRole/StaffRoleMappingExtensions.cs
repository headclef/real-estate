using Realestate.Domain.Entities.Identity;
using Realestate.Application.DTOs.StaffRole;
namespace Realestate.Application.Mappings.StaffRole;

public static class StaffRoleMappingExtensions
{
    public static StaffRoleDto ToDto(this Realestate.Domain.Entities.Identity.StaffRole src)
    {
        if (src == null) return null!;
        return new StaffRoleDto { Id = src.Id, Name = src.Name };
    }

    public static Realestate.Domain.Entities.Identity.StaffRole ToEntity(this CreateStaffRoleDto src)
    {
        if (src == null) return null!;
        return new Realestate.Domain.Entities.Identity.StaffRole { Name = src.Name };
    }

    public static void UpdateFrom(this Realestate.Domain.Entities.Identity.StaffRole target, UpdateStaffRoleDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}