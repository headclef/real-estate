using Microsoft.AspNetCore.Mvc;
using Realestate.Application.Wrappers;
namespace Realestate.API.Controllers;

/// <summary>
/// Base controller that maps <see cref="Response"/> wrappers to proper HTTP status codes.
/// All API controllers should inherit from this instead of <see cref="ControllerBase"/>.
/// </summary>
[ApiController]
public abstract class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Converts a <see cref="Response"/> wrapper into the appropriate <see cref="IActionResult"/>:
    ///   Success → 200 OK (or custom status)
    ///   Failure → StatusCode from wrapper (400 / 404 / 422 / 500)
    /// </summary>
    protected IActionResult ApiResponse(Response response)
    {
        if (response.Success)
            return Ok(response);

        return StatusCode(response.StatusCode ?? 400, response);
    }

    /// <summary>Generic typed overload.</summary>
    protected IActionResult ApiResponse<T>(Response<T> response)
    {
        if (response.Success)
            return Ok(response);

        return StatusCode(response.StatusCode ?? 400, response);
    }

    /// <summary>
    /// For POST/Create — returns 201 Created with a Location header when successful.
    /// </summary>
    protected IActionResult CreatedResponse<T>(Response<T> response, string actionName, object routeValues)
    {
        if (response.Success)
            return CreatedAtAction(actionName, routeValues, response);

        return StatusCode(response.StatusCode ?? 400, response);
    }
}