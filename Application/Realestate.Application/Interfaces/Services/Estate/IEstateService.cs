using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.Estate;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.Estate;

public interface IEstateService
{
    Task<Response<EstateDto>> GetByIdAsync(int id);
    Task<PagedResponse<EstateDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<EstateDto>> CreateAsync(CreateEstateDto dto);
    Task<Response<EstateDto>> UpdateAsync(int id, UpdateEstateDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}