using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Realestate.Application.DTOs.Country;
using Realestate.Application.Interfaces.Services.Country;
namespace Realestate.API.Controllers;

/// <summary>
/// Manages countries (World schema).
/// </summary>
[Tags("World")]
[Authorize]
public class CountryController : ApiBaseController
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    /// <summary>Get a country by its identifier.</summary>
    /// <param name="id">Country identifier.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _countryService.GetByIdAsync(id);
        return ApiResponse(result);
    }

    /// <summary>List countries with pagination.</summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _countryService.ListAsync(page, pageSize);
        return ApiResponse(result);
    }

    /// <summary>Create a new country.</summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCountryDto dto)
    {
        var result = await _countryService.CreateAsync(dto);
        return CreatedResponse(result, nameof(GetById), new { id = result.Data?.Id });
    }

    /// <summary>Update an existing country.</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryDto dto)
    {
        var result = await _countryService.UpdateAsync(id, dto);
        return ApiResponse(result);
    }

    /// <summary>Soft-delete a country.</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _countryService.DeleteAsync(id);
        return ApiResponse(result);
    }
}