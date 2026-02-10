using Realestate.Application.DTOs.Staff;
using Realestate.Application.Interfaces.Services.Staff;
using Realestate.Application.Interfaces.Repositories.Staff;
using Realestate.Application.Mappings.Staff;
using Realestate.Application.Validation.Staff;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _staffRepository;
    private readonly CreateStaffValidator _createValidator = new();
    private readonly UpdateStaffValidator _updateValidator = new();

    public StaffService(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<Response<StaffDto>> GetByIdAsync(int id)
    {
        var entity = await _staffRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<StaffDto>("Staff not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<StaffDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _staffRepository.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<StaffDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<StaffDto>> CreateAsync(CreateStaffDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<StaffDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _staffRepository.AddAsync(entity);
        return Response.Ok(entity.ToDto(), "Staff created successfully.");
    }

    public async Task<Response<StaffDto>> UpdateAsync(int id, UpdateStaffDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<StaffDto>(validation.Errors!, statusCode: 400);

        var entity = await _staffRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<StaffDto>("Staff not found.");

        entity.UpdateFrom(dto);
        await _staffRepository.UpdateAsync(entity);

        return Response.Ok(entity.ToDto(), "Staff updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _staffRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("Staff not found.");

        await _staffRepository.DeleteAsync(entity);
        return Response.Ok(true, "Staff deleted successfully.");
    }
}