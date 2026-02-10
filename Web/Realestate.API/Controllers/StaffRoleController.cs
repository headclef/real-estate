using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.StaffRole;
using Realestate.Application.Interfaces.Services.StaffRole;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages staff roles — Agent, Manager, Admin, etc. (Identity schema).
/// </summary>
[Route("api/[controller]")]
[Tags("Identity")]
public class StaffRoleController : ApiBaseController
{
    private readonly IStaffRoleService _staffRoleService;

    public StaffRoleController(IStaffRoleService staffRoleService)
    {
        _staffRoleService = staffRoleService;
    }

    /// <summary>Get a staff role by its identifier.</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _staffRoleService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List staff roles with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _staffRoleService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new staff role.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateStaffRoleDto dto)
    {
        var result = await _staffRoleService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing staff role.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffRoleDto dto)
    {
        var result = await _staffRoleService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete a staff role.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _staffRoleService.DeleteAsync(id);
        return ApiResponse(result);
    }
}