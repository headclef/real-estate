using Realestate.Application.DTOs.Division;
using Realestate.Application.Interfaces.Services.Division;
using Realestate.Application.Interfaces.Repositories.Division;
using Realestate.Application.Mappings.Division;
using Realestate.Application.Validation.Division;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class DivisionService : IDivisionService
{
    private readonly IDivisionRepository _divisionRepository;
    private readonly CreateDivisionValidator _createValidator = new();
    private readonly UpdateDivisionValidator _updateValidator = new();

    public DivisionService(IDivisionRepository divisionRepository)
    {
        _divisionRepository = divisionRepository;
    }

    public async Task<Response<DivisionDto>> GetByIdAsync(int id)
    {
        var entity = await _divisionRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<DivisionDto>("Division not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<DivisionDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _divisionRepository.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<DivisionDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<DivisionDto>> CreateAsync(CreateDivisionDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<DivisionDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _divisionRepository.AddAsync(entity);
        return Response.Ok(entity.ToDto(), "Division created successfully.");
    }

    public async Task<Response<DivisionDto>> UpdateAsync(int id, UpdateDivisionDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<DivisionDto>(validation.Errors!, statusCode: 400);

        var entity = await _divisionRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<DivisionDto>("Division not found.");

        entity.UpdateFrom(dto);
        await _divisionRepository.UpdateAsync(entity);

        return Response.Ok(entity.ToDto(), "Division updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _divisionRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("Division not found.");

        await _divisionRepository.DeleteAsync(entity);
        return Response.Ok(true, "Division deleted successfully.");
    }
}