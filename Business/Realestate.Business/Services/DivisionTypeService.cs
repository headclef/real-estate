using Realestate.Application.DTOs.DivisionType;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.DivisionType;
using Realestate.Application.Mappings.DivisionType;
using Realestate.Application.Validation.DivisionType;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class DivisionTypeService : IDivisionTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateDivisionTypeValidator _createValidator = new();
    private readonly UpdateDivisionTypeValidator _updateValidator = new();

    public DivisionTypeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<DivisionTypeDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.DivisionTypes.GetByIdAsync(id);
        if (entity == null) return Response.Fail<DivisionTypeDto>("DivisionType not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<DivisionTypeDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _unitOfWork.DivisionTypes.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<DivisionTypeDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<DivisionTypeDto>> CreateAsync(CreateDivisionTypeDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<DivisionTypeDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _unitOfWork.DivisionTypes.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(entity.ToDto(), "DivisionType created successfully.");
    }

    public async Task<Response<DivisionTypeDto>> UpdateAsync(int id, UpdateDivisionTypeDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<DivisionTypeDto>(validation.Errors!, statusCode: 400);

        var entity = await _unitOfWork.DivisionTypes.GetByIdAsync(id);
        if (entity == null) return Response.Fail<DivisionTypeDto>("DivisionType not found.");

        entity.UpdateFrom(dto);
        await _unitOfWork.DivisionTypes.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Response.Ok(entity.ToDto(), "DivisionType updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.DivisionTypes.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("DivisionType not found.");

        await _unitOfWork.DivisionTypes.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(true, "DivisionType deleted successfully.");
    }
}