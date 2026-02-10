using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.EstateType;
using Realestate.Application.Interfaces.Services.EstateType;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages estate types — Apartment, Villa, Land, etc. (Property schema).
/// </summary>
[Tags("Property")]
[Authorize]
public class EstateTypeController : ApiBaseController
{
    private readonly IEstateTypeService _estateTypeService;

    public EstateTypeController(IEstateTypeService estateTypeService)
    {
        _estateTypeService = estateTypeService;
    }

    /// <summary>Get an estate type by its identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _estateTypeService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List estate types with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _estateTypeService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new estate type.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEstateTypeDto dto)
    {
        var result = await _estateTypeService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing estate type.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstateTypeDto dto)
    {
        var result = await _estateTypeService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete an estate type.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estateTypeService.DeleteAsync(id);
        return ApiResponse(result);
    }
}