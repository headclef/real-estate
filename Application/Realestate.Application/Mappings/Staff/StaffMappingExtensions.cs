using Realestate.Domain.Entities;
using Realestate.Application.DTOs;
namespace Realestate.Application.Mappings;

public static class StaffMappingExtensions
{
    public static StaffDto ToDto(this Staff src)
    {
        if (src == null) return null!;
        return new StaffDto
        {
            Id = src.Id,
            Name = src.Name,
            Surname = src.Surname,
            FullName = src.FullName,
            Code = src.Code,
            Email = src.Email,
            PhoneNumber = src.PhoneNumber,
            StaffRoleId = src.StaffRoleId
        };
    }

    public static Staff ToEntity(this CreateStaffDto src)
    {
        if (src == null) return null!;
        return new Staff
        {
            Name = src.Name,
            Surname = src.Surname,
            Code = src.Code,
            Email = src.Email,
            PhoneNumber = src.PhoneNumber,
            StaffRoleId = src.StaffRoleId
        };
    }

    public static void UpdateFrom(this Staff target, UpdateStaffDto src)
    {
        if (target == null || src == null) return;
        target.Name = src.Name;
        target.Surname = src.Surname;
        target.Code = src.Code;
        target.Email = src.Email;
        target.PhoneNumber = src.PhoneNumber;
        target.StaffRoleId = src.StaffRoleId;
    }
}