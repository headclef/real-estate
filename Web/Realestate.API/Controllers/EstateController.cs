using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Estate;
using Realestate.Application.Interfaces.Services.Estate;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages real-estate listings (Property schema).
/// </summary>
[Route("api/[controller]")]
[Tags("Property")]
public class EstateController : ApiBaseController
{
    private readonly IEstateService _estateService;

    public EstateController(IEstateService estateService)
    {
        _estateService = estateService;
    }

    /// <summary>Get an estate by its identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _estateService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List estates with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _estateService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new estate listing.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEstateDto dto)
    {
        var result = await _estateService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing estate.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstateDto dto)
    {
        var result = await _estateService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete an estate listing.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estateService.DeleteAsync(id);
        return ApiResponse(result);
    }
}