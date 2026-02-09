using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.EstateStatus;

public interface IEstateStatusService
{
    Task<Response<EstateStatusDto>> GetByIdAsync(int id);
    Task<PagedResponse<EstateStatusDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<EstateStatusDto>> CreateAsync(CreateEstateStatusDto dto);
    Task<Response<EstateStatusDto>> UpdateAsync(int id, UpdateEstateStatusDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}