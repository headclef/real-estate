using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.EstateType;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.EstateType;

public interface IEstateTypeService
{
    Task<Response<EstateTypeDto>> GetByIdAsync(int id);
    Task<PagedResponse<EstateTypeDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<EstateTypeDto>> CreateAsync(CreateEstateTypeDto dto);
    Task<Response<EstateTypeDto>> UpdateAsync(int id, UpdateEstateTypeDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}