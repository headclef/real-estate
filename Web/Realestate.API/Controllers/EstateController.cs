using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Estate;
using Realestate.Application.Interfaces.Services.Estate;
namespace Realestate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstateController : ControllerBase
{
    private readonly IEstateService _estateService;

    public EstateController(IEstateService estateService)
    {
        _estateService = estateService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _estateService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _estateService.ListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEstateDto dto)
    {
        var result = await _estateService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstateDto dto)
    {
        var result = await _estateService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estateService.DeleteAsync(id);
        return Ok(result);
    }
}