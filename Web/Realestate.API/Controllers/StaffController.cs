using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Staff;
using Realestate.Application.Interfaces.Services.Staff;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages staff members (Identity schema).
/// </summary>
[Route("api/[controller]")]
[Tags("Identity")]
[Authorize]
public class StaffController : ApiBaseController
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    /// <summary>Get a staff member by their identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _staffService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List staff members with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _staffService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new staff member.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateStaffDto dto)
    {
        var result = await _staffService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing staff member.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffDto dto)
    {
        var result = await _staffService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete a staff member.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _staffService.DeleteAsync(id);
        return ApiResponse(result);
    }
}