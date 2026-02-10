using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.EstateType;
using Realestate.Application.Interfaces.Services.EstateType;
namespace Realestate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstateTypeController : ControllerBase
{
    private readonly IEstateTypeService _estateTypeService;

    public EstateTypeController(IEstateTypeService estateTypeService)
    {
        _estateTypeService = estateTypeService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _estateTypeService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _estateTypeService.ListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEstateTypeDto dto)
    {
        var result = await _estateTypeService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstateTypeDto dto)
    {
        var result = await _estateTypeService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estateTypeService.DeleteAsync(id);
        return Ok(result);
    }
}