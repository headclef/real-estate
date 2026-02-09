using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.Division;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.Division;

public interface IDivisionService
{
    Task<Response<DivisionDto>> GetByIdAsync(int id);
    Task<PagedResponse<DivisionDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<DivisionDto>> CreateAsync(CreateDivisionDto dto);
    Task<Response<DivisionDto>> UpdateAsync(int id, UpdateDivisionDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}