using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.DivisionType;
using Realestate.Application.Interfaces.Services.DivisionType;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages division types — Province, District, Neighborhood, etc. (World schema).
/// </summary>
[Route("api/[controller]")]
[Tags("World")]
[Authorize]
public class DivisionTypeController : ApiBaseController
{
    private readonly IDivisionTypeService _divisionTypeService;

    public DivisionTypeController(IDivisionTypeService divisionTypeService)
    {
        _divisionTypeService = divisionTypeService;
    }

    /// <summary>Get a division type by its identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _divisionTypeService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List division types with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _divisionTypeService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new division type.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDivisionTypeDto dto)
    {
        var result = await _divisionTypeService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing division type.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDivisionTypeDto dto)
    {
        var result = await _divisionTypeService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete a division type.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _divisionTypeService.DeleteAsync(id);
        return ApiResponse(result);
    }
}