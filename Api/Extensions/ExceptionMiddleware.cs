using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Api.Extensions;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try { await next(ctx); }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error: {Msg}", ex.Message);
            await HandleAsync(ctx, ex);
        }
    }

    private static Task HandleAsync(HttpContext ctx, Exception ex)
    {
        var (status, message) = ex switch
        {
            ValidationException v    => (HttpStatusCode.BadRequest,  string.Join(", ", v.Errors.Select(e => e.ErrorMessage))),
            KeyNotFoundException     => (HttpStatusCode.NotFound,    ex.Message),
            InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ex.Message),
            _                        => (HttpStatusCode.InternalServerError, "Error interno del servidor.")
        };
        ctx.Response.ContentType = "application/json";
        ctx.Response.StatusCode  = (int)status;
        return ctx.Response.WriteAsync(JsonSerializer.Serialize(new
            { StatusCode = (int)status, Message = message, Timestamp = DateTime.UtcNow }));
    }
}
