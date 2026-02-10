using Realestate.Application.DTOs.Estate;
using Realestate.Application.Interfaces.Services.Estate;
using Realestate.Application.Interfaces.Repositories.Estate;
using Realestate.Application.Mappings.Estate;
using Realestate.Application.Validation.Estate;
using Realestate.Application.Wrappers;
namespace Realestate.Business.Services;

public class EstateService : IEstateService
{
    private readonly IEstateRepository _estateRepository;
    private readonly CreateEstateValidator _createValidator = new();
    private readonly UpdateEstateValidator _updateValidator = new();

    public EstateService(IEstateRepository estateRepository)
    {
        _estateRepository = estateRepository;
    }

    public async Task<Response<EstateDto>> GetByIdAsync(int id)
    {
        var entity = await _estateRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateDto>("Estate not found.");
        return Response.Ok(entity.ToDto());
    }

    public async Task<PagedResponse<EstateDto>> ListAsync(int page = 1, int pageSize = 20)
    {
        var entities = await _estateRepository.GetAllAsync();
        var dtos = entities.Select(e => e.ToDto()).ToList();

        var pagedData = dtos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return PagedResponse<EstateDto>.Ok(pagedData, dtos.Count, page, pageSize);
    }

    public async Task<Response<EstateDto>> CreateAsync(CreateEstateDto dto)
    {
        var validation = _createValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateDto>(validation.Errors!, statusCode: 400);

        var entity = dto.ToEntity();
        await _estateRepository.AddAsync(entity);
        return Response.Ok(entity.ToDto(), "Estate created successfully.");
    }

    public async Task<Response<EstateDto>> UpdateAsync(int id, UpdateEstateDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsSuccess)
            return Response.Fail<EstateDto>(validation.Errors!, statusCode: 400);

        var entity = await _estateRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<EstateDto>("Estate not found.");

        entity.UpdateFrom(dto);
        await _estateRepository.UpdateAsync(entity);

        return Response.Ok(entity.ToDto(), "Estate updated successfully.");
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var entity = await _estateRepository.GetByIdAsync(id);
        if (entity == null) return Response.Fail<bool>("Estate not found.");

        await _estateRepository.DeleteAsync(entity);
        return Response.Ok(true, "Estate deleted successfully.");
    }
}