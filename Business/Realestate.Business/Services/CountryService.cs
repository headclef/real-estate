using Realestate.Application.DTOs.Country;
using Realestate.Application.Interfaces.Services.Country;
using Realestate.Application.Interfaces.Repositories.Country;
using Realestate.Application.Mappings.Country;
using Realestate.Application.Wrappers;
using Realestate.Domain.Entities;
namespace Realestate.Business.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Response<CountryDto>> GetByIdAsync(int id)
    {
        var entity = await _countryRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<CountryDto>("Country not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<CountryDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _countryRepository.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();
        
        // Simplified paging for demonstration
        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        return PagedResponse<CountryDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<CountryDto>> CreateAsync(CreateCountryDto dto)
    {
        var entity = dto.ToEntity();
        await _countryRepository.AddAsync(entity);
        return Response.Ok(entity.ToDto(), "Country created successfully.");
    }

    public async Task<Response<CountryDto>> UpdateAsync(int id, UpdateCountryDto dto)
    {
        var entity = await _countryRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<CountryDto>("Country not found.");

        entity.UpdateFrom(dto);
        await _countryRepository.UpdateAsync(entity);
        
        return Response.Ok(entity.ToDto(), "Country updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _countryRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("Country not found.");

        await _countryRepository.DeleteAsync(entity);
        return Response.Ok(true, "Country deleted successfully.");
    }
}