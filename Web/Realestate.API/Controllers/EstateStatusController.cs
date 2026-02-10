using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Interfaces.Services.EstateStatus;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages estate statuses — ForSale, ForRent, Sold, etc. (Property schema).
/// </summary>
[Route("api/[controller]")]
[Tags("Property")]
public class EstateStatusController : ApiBaseController
{
    private readonly IEstateStatusService _estateStatusService;

    public EstateStatusController(IEstateStatusService estateStatusService)
    {
        _estateStatusService = estateStatusService;
    }

    /// <summary>Get an estate status by its identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _estateStatusService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List estate statuses with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _estateStatusService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new estate status.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateEstateStatusDto dto)
    {
        var result = await _estateStatusService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing estate status.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstateStatusDto dto)
    {
        var result = await _estateStatusService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete an estate status.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estateStatusService.DeleteAsync(id);
        return ApiResponse(result);
    }
}