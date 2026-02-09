using System;
using System.Collections.Generic;
using System.Linq;
namespace Realestate.Application.Wrappers;

public class Response
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    public int? StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsSuccess => Success;

    public static Response Fail(IEnumerable<string> errors, string? message = null, int? statusCode = null)
        => new Response { Success = false, Errors = errors?.ToList(), Message = message, StatusCode = statusCode };

    public static Response Fail(string error, int? statusCode = null)
        => Fail(new[] { error }, null, statusCode);

    public static Response Ok(string? message = null)
        => new Response { Success = true, Message = message };

    public static Response FromException(Exception ex, string? message = null, int? statusCode = 500)
        => new Response { Success = false, Message = message ?? ex.Message, Errors = new[] { ex.ToString() }, StatusCode = statusCode };
}

public class Response<T> : Response
{
    public T? Data { get; set; }

    public static Response<T> Ok(T data, string? message = null)
        => new Response<T> { Success = true, Data = data, Message = message };

    public static Response<T> Ok(string? message = null)
        => new Response<T> { Success = true, Message = message };

    public static Response<T> Fail(IEnumerable<string> errors, string? message = null, int? statusCode = null)
        => new Response<T> { Success = false, Errors = errors?.ToList(), Message = message, StatusCode = statusCode };

    public static Response<T> Fail(string error, int? statusCode = null)
        => Fail(new[] { error }, null, statusCode);

    public static Response<T> FromException(Exception ex, int? statusCode = 500)
        => new Response<T> { Success = false, Message = ex.Message, Errors = new[] { ex.ToString() }, StatusCode = statusCode };
}

public class PagedResponse<T> : Response<IEnumerable<T>>
{
    public long Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }

    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(Total / (double)PageSize);

    public static PagedResponse<T> Ok(IEnumerable<T> data, long total, int page, int pageSize, string? message = null)
        => new PagedResponse<T>
        {
            Success = true,
            Data = data,
            Total = total,
            Page = page,
            PageSize = pageSize,
            Message = message
        };

    public static new PagedResponse<T> Fail(IEnumerable<string> errors, string? message = null, int? statusCode = null)
        => new PagedResponse<T> { Success = false, Errors = errors?.ToList(), Message = message, StatusCode = statusCode };
}