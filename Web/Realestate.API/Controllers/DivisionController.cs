using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Division;
using Realestate.Application.Interfaces.Services.Division;
namespace Realestate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DivisionController : ControllerBase
{
    private readonly IDivisionService _divisionService;

    public DivisionController(IDivisionService divisionService)
    {
        _divisionService = divisionService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _divisionService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _divisionService.ListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDivisionDto dto)
    {
        var result = await _divisionService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDivisionDto dto)
    {
        var result = await _divisionService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _divisionService.DeleteAsync(id);
        return Ok(result);
    }
}