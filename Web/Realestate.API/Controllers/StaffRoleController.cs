using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.StaffRole;
using Realestate.Application.Interfaces.Services.StaffRole;
namespace Realestate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StaffRoleController : ControllerBase
{
    private readonly IStaffRoleService _staffRoleService;

    public StaffRoleController(IStaffRoleService staffRoleService)
    {
        _staffRoleService = staffRoleService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _staffRoleService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _staffRoleService.ListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStaffRoleDto dto)
    {
        var result = await _staffRoleService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateStaffRoleDto dto)
    {
        var result = await _staffRoleService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _staffRoleService.DeleteAsync(id);
        return Ok(result);
    }
}