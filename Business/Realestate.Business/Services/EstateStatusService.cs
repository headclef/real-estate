using Microsoft.Extensions.Logging;
using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.EstateStatus;
using Realestate.Application.Mappings.EstateStatus;
using Realestate.Application.Validation.EstateStatus;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class EstateStatusService : IEstateStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EstateStatusService> _logger;
    private readonly CreateEstateStatusValidator _createValidator = new();
    private readonly UpdateEstateStatusValidator _updateValidator = new();

    public EstateStatusService(IUnitOfWork unitOfWork, ILogger<EstateStatusService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<EstateStatusDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.EstateStatuses.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("EstateStatus with Id {EstateStatusId} not found", id);
            return Response.Fail<EstateStatusDto>("EstateStatus not found.");
        }
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<EstateStatusDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _unitOfWork.EstateStatuses.GetAllAsync();
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
        await _unitOfWork.EstateStatuses.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("EstateStatus created with Id {EstateStatusId}", entity.Id);
        return Response.Ok(entity.ToDto(), "EstateStatus created successfully.");
    }

    public async Task<Response<EstateStatusDto>> UpdateAsync(int id, UpdateEstateStatusDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateStatusDto>(validation.Errors!, statusCode: 400);

        var entity = await _unitOfWork.EstateStatuses.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("EstateStatus with Id {EstateStatusId} not found for update", id);
            return Response.Fail<EstateStatusDto>("EstateStatus not found.");
        }

        entity.UpdateFrom(dto);
        await _unitOfWork.EstateStatuses.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("EstateStatus with Id {EstateStatusId} updated", id);
        return Response.Ok(entity.ToDto(), "EstateStatus updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.EstateStatuses.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("EstateStatus with Id {EstateStatusId} not found for deletion", id);
            return Response.Fail<bool>("EstateStatus not found.");
        }

        await _unitOfWork.EstateStatuses.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("EstateStatus with Id {EstateStatusId} soft-deleted", id);
        return Response.Ok(true, "EstateStatus deleted successfully.");
    }
}