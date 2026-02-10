using Realestate.Application.DTOs.EstateType;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.EstateType;
using Realestate.Application.Mappings.EstateType;
using Realestate.Application.Validation.EstateType;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class EstateTypeService : IEstateTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateEstateTypeValidator _createValidator = new();
    private readonly UpdateEstateTypeValidator _updateValidator = new();

    public EstateTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<EstateTypeDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.EstateTypes.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateTypeDto>("EstateType not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<EstateTypeDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _unitOfWork.EstateTypes.GetAllAsync();
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
        await _unitOfWork.EstateTypes.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(entity.ToDto(), "EstateType created successfully.");
    }

    public async Task<Response<EstateTypeDto>> UpdateAsync(int id, UpdateEstateTypeDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateTypeDto>(validation.Errors!, statusCode: 400);

        var entity = await _unitOfWork.EstateTypes.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateTypeDto>("EstateType not found.");

        entity.UpdateFrom(dto);
        await _unitOfWork.EstateTypes.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Response.Ok(entity.ToDto(), "EstateType updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.EstateTypes.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("EstateType not found.");

        await _unitOfWork.EstateTypes.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(true, "EstateType deleted successfully.");
    }
}