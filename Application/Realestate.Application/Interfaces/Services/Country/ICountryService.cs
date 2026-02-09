using System.Collections.Generic;
using System.Threading.Tasks;
using Realestate.Application.DTOs.Country;
using Realestate.Application.Wrappers;
namespace Realestate.Application.Interfaces.Services.Country;

public interface ICountryService
{
    Task<Response<CountryDto>> GetByIdAsync(int id);
    Task<PagedResponse<CountryDto>> ListAsync(int page = 1, int pageSize = 20);
    Task<Response<CountryDto>> CreateAsync(CreateCountryDto dto);
    Task<Response<CountryDto>> UpdateAsync(int id, UpdateCountryDto dto);
    Task<Response<bool>> DeleteAsync(int id);
}