using Realestate.Application.DTOs.StaffRole;
using Realestate.Application.Interfaces;
using Realestate.Application.Interfaces.Services.StaffRole;
using Realestate.Application.Mappings.StaffRole;
using Realestate.Application.Validation.StaffRole;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class StaffRoleService : IStaffRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateStaffRoleValidator _createValidator = new();
    private readonly UpdateStaffRoleValidator _updateValidator = new();

    public StaffRoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<StaffRoleDto>> GetByIdAsync(int id)
    {
        var entity = await _unitOfWork.StaffRoles.GetByIdAsync(id);
        if (entity == null) return Response.Fail<StaffRoleDto>("StaffRole not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<StaffRoleDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _unitOfWork.StaffRoles.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<StaffRoleDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<StaffRoleDto>> CreateAsync(CreateStaffRoleDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<StaffRoleDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _unitOfWork.StaffRoles.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(entity.ToDto(), "StaffRole created successfully.");
    }

    public async Task<Response<StaffRoleDto>> UpdateAsync(int id, UpdateStaffRoleDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<StaffRoleDto>(validation.Errors!, statusCode: 400);

        var entity = await _unitOfWork.StaffRoles.GetByIdAsync(id);
        if (entity == null) return Response.Fail<StaffRoleDto>("StaffRole not found.");

        entity.UpdateFrom(dto);
        await _unitOfWork.StaffRoles.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Response.Ok(entity.ToDto(), "StaffRole updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _unitOfWork.StaffRoles.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("StaffRole not found.");

        await _unitOfWork.StaffRoles.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();
        return Response.Ok(true, "StaffRole deleted successfully.");
    }
}