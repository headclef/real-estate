using Realestate.Application.DTOs.Country;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.Country;
using Realestate.Application.Mappings.Country;
using Realestate.Application.Validation.Country;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateCountryValidator _createValidator = new();
    private readonly UpdateCountryValidator _updateValidator = new();

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<CountryDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Countries.GetByIdAsync(id);
        if (entity == null) return Response.Fail<CountryDto>("Country not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<CountryDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _unitOfWork.Countries.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();
        
        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        return PagedResponse<CountryDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<CountryDto>> CreateAsync(CreateCountryDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<CountryDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _unitOfWork.Countries.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(entity.ToDto(), "Country created successfully.");
    }

    public async Task<Response<CountryDto>> UpdateAsync(int id, UpdateCountryDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<CountryDto>(validation.Errors!, statusCode: 400);

        var entity = await _unitOfWork.Countries.GetByIdAsync(id);
        if (entity == null) return Response.Fail<CountryDto>("Country not found.");

        entity.UpdateFrom(dto);
        await _unitOfWork.Countries.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        
        return Response.Ok(entity.ToDto(), "Country updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Countries.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("Country not found.");

        await _unitOfWork.Countries.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(true, "Country deleted successfully.");
    }
}