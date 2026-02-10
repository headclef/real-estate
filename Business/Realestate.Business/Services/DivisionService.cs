using Microsoft.Extensions.Logging;
using Realestate.Application.DTOs.Division;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.Division;
using Realestate.Application.Mappings.Division;
using Realestate.Application.Validation.Division;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class DivisionService : IDivisionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DivisionService> _logger;
    private readonly CreateDivisionValidator _createValidator = new();
    private readonly UpdateDivisionValidator _updateValidator = new();

    public DivisionService(IUnitOfWork unitOfWork, ILogger<DivisionService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Response<DivisionDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.Divisions.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("Division with Id {DivisionId} not found", id);
            return Response.Fail<DivisionDto>("Division not found.");
        }
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<DivisionDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _unitOfWork.Divisions.GetAllAsync();
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
        await _unitOfWork.Divisions.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Division created with Id {DivisionId}", entity.Id);
        return Response.Ok(entity.ToDto(), "Division created successfully.");
    }

    public async Task<Response<DivisionDto>> UpdateAsync(int id, UpdateDivisionDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<DivisionDto>(validation.Errors!, statusCode: 400);

        var entity = await _unitOfWork.Divisions.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("Division with Id {DivisionId} not found for update", id);
            return Response.Fail<DivisionDto>("Division not found.");
        }

        entity.UpdateFrom(dto);
        await _unitOfWork.Divisions.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Division with Id {DivisionId} updated", id);
        return Response.Ok(entity.ToDto(), "Division updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.Divisions.GetByIdAsync(id);
        if (entity == null)
        {
            _logger.LogWarning("Division with Id {DivisionId} not found for deletion", id);
            return Response.Fail<bool>("Division not found.");
        }

        await _unitOfWork.Divisions.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Division with Id {DivisionId} soft-deleted", id);
        return Response.Ok(true, "Division deleted successfully.");
    }
}