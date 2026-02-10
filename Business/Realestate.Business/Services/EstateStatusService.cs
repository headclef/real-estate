using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Interfaces.Services.EstateStatus;
using Realestate.Application.Interfaces.Repositories.EstateStatus;
using Realestate.Application.Mappings.EstateStatus;
using Realestate.Application.Validation.EstateStatus;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class EstateStatusService : IEstateStatusService
{
    private readonly IEstateStatusRepository _estateStatusRepository;
    private readonly CreateEstateStatusValidator _createValidator = new();
    private readonly UpdateEstateStatusValidator _updateValidator = new();

    public EstateStatusService(IEstateStatusRepository estateStatusRepository)
    {
        _estateStatusRepository = estateStatusRepository;
    }

    public async Task<Response<EstateStatusDto>> GetByIdAsync(int id)
    {
        var entity = await _estateStatusRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateStatusDto>("EstateStatus not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<EstateStatusDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _estateStatusRepository.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<EstateStatusDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<EstateStatusDto>> CreateAsync(CreateEstateStatusDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateStatusDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _estateStatusRepository.AddAsync(entity);
        return Response.Ok(entity.ToDto(), "EstateStatus created successfully.");
    }

    public async Task<Response<EstateStatusDto>> UpdateAsync(int id, UpdateEstateStatusDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateStatusDto>(validation.Errors!, statusCode: 400);

        var entity = await _estateStatusRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateStatusDto>("EstateStatus not found.");

        entity.UpdateFrom(dto);
        await _estateStatusRepository.UpdateAsync(entity);

        return Response.Ok(entity.ToDto(), "EstateStatus updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _estateStatusRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("EstateStatus not found.");

        await _estateStatusRepository.DeleteAsync(entity);
        return Response.Ok(true, "EstateStatus deleted successfully.");
    }
}