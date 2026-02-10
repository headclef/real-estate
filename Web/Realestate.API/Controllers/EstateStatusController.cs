using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.EstateStatus;
using Realestate.Application.Interfaces.Services.EstateStatus;
namespace Realestate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstateStatusController : ControllerBase
{
    private readonly IEstateStatusService _estateStatusService;

    public EstateStatusController(IEstateStatusService estateStatusService)
    {
        _estateStatusService = estateStatusService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _estateStatusService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _estateStatusService.ListAsync(page, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEstateStatusDto dto)
    {
        var result = await _estateStatusService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEstateStatusDto dto)
    {
        var result = await _estateStatusService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _estateStatusService.DeleteAsync(id);
        return Ok(result);
    }
}