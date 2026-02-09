using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces;

public interface IStaffService
{
    Task<Response<StaffDto>> GetByIdAsync(int id);
    Task<PagedResponse<StaffDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<StaffDto>> CreateAsync(CreateStaffDto dto);
    Task<Response<StaffDto>> UpdateAsync(int id, UpdateStaffDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}