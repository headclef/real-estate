using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.DivisionType;
using Realestate.Application.Interfaces.Services.DivisionType;
namespace Realestate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DivisionTypeController : ControllerBase
{
    private readonly IDivisionTypeService _divisionTypeService;

    public DivisionTypeController(IDivisionTypeService divisionTypeService)
    {
        _divisionTypeService = divisionTypeService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _divisionTypeService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _divisionTypeService.ListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDivisionTypeDto dto)
    {
        var result = await _divisionTypeService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDivisionTypeDto dto)
    {
        var result = await _divisionTypeService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _divisionTypeService.DeleteAsync(id);
        return Ok(result);
    }
}