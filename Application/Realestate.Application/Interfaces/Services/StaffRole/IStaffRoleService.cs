using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.StaffRole;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.StaffRole;

public interface IStaffRoleService
{
    Task<Response<StaffRoleDto>> GetByIdAsync(int id);
    Task<PagedResponse<StaffRoleDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<StaffRoleDto>> CreateAsync(CreateStaffRoleDto dto);
    Task<Response<StaffRoleDto>> UpdateAsync(int id, UpdateStaffRoleDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}