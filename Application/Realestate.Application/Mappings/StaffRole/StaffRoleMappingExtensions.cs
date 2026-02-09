using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class StaffRoleMappingExtensions
{
    public static StaffRoleDto ToDto(this StaffRole src)
    {
        if (src == null) return null!;
        return new StaffRoleDto { Id = src.Id, Name = src.Name };
    }

    public static StaffRole ToEntity(this CreateStaffRoleDto src)
    {
        if (src == null) return null!;
        return new StaffRole { Name = src.Name };
    }

    public static void UpdateFrom(this StaffRole target, UpdateStaffRoleDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
    }
}