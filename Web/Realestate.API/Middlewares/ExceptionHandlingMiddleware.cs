using System.Net;
using System.Text.Json;
using Realestate.Application.Exceptions;
using Realestate.Application.Wrappers;
namespace Realestate.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, response) = exception switch
        {
            NotFoundException notFound =>
                ((int)HttpStatusCode.NotFound,
                 Response.Fail(notFound.Message, (int)HttpStatusCode.NotFound)),

            ValidationException validation =>
                ((int)HttpStatusCode.BadRequest,
                 Response.Fail(validation.Errors, statusCode: (int)HttpStatusCode.BadRequest)),

            BusinessRuleException business =>
                ((int)HttpStatusCode.UnprocessableEntity,
                 Response.Fail(business.Message, (int)HttpStatusCode.UnprocessableEntity)),

            _ =>
                ((int)HttpStatusCode.InternalServerError,
                 Response.Fail("An unexpected error occurred.", (int)HttpStatusCode.InternalServerError))
        };

        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonOptions));
    }
}