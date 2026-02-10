using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Division;
using Realestate.Application.Interfaces.Services.Division;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages administrative divisions — provinces, districts, etc. (World schema).
/// </summary>
[Tags("World")]
[Authorize]
public class DivisionController : ApiBaseController
{
    private readonly IDivisionService _divisionService;

    public DivisionController(IDivisionService divisionService)
    {
        _divisionService = divisionService;
    }

    /// <summary>Get a division by its identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _divisionService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List divisions with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _divisionService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new division.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateDivisionDto dto)
    {
        var result = await _divisionService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing division.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDivisionDto dto)
    {
        var result = await _divisionService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete a division.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _divisionService.DeleteAsync(id);
        return ApiResponse(result);
    }
}