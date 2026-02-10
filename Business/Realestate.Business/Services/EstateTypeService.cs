using Realestate.Application.DTOs.EstateType;
using Realestate.Application.Interfaces.Services.EstateType;
using Realestate.Application.Interfaces.Repositories.EstateType;
using Realestate.Application.Mappings.EstateType;
using Realestate.Application.Validation.EstateType;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class EstateTypeService : IEstateTypeService
{
    private readonly IEstateTypeRepository _estateTypeRepository;
    private readonly CreateEstateTypeValidator _createValidator = new();
    private readonly UpdateEstateTypeValidator _updateValidator = new();

    public EstateTypeService(IEstateTypeRepository estateTypeRepository)
    {
        _estateTypeRepository = estateTypeRepository;
    }

    public async Task<Response<EstateTypeDto>> GetByIdAsync(int id)
    {
        var entity = await _estateTypeRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateTypeDto>("EstateType not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<EstateTypeDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _estateTypeRepository.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<EstateTypeDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<EstateTypeDto>> CreateAsync(CreateEstateTypeDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateTypeDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _estateTypeRepository.AddAsync(entity);
        return Response.Ok(entity.ToDto(), "EstateType created successfully.");
    }

    public async Task<Response<EstateTypeDto>> UpdateAsync(int id, UpdateEstateTypeDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateTypeDto>(validation.Errors!, statusCode: 400);

        var entity = await _estateTypeRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateTypeDto>("EstateType not found.");

        entity.UpdateFrom(dto);
        await _estateTypeRepository.UpdateAsync(entity);

        return Response.Ok(entity.ToDto(), "EstateType updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _estateTypeRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("EstateType not found.");

        await _estateTypeRepository.DeleteAsync(entity);
        return Response.Ok(true, "EstateType deleted successfully.");
    }
}