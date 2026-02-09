using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.DivisionType;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.DivisionType;

public interface IDivisionTypeService
{
    Task<Response<DivisionTypeDto>> GetByIdAsync(int id);
    Task<PagedResponse<DivisionTypeDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<DivisionTypeDto>> CreateAsync(CreateDivisionTypeDto dto);
    Task<Response<DivisionTypeDto>> UpdateAsync(int id, UpdateDivisionTypeDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}