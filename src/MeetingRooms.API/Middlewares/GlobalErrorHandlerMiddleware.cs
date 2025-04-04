using MeetingRooms.API.Resources;
using MeetingRooms.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace MeetingRooms.API.Middlewares;

public class GlobalErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (ex is ServiceException serviceException)
                await HandleExceptionAsync(context, serviceException);
            else
                await HandleExceptionAsync(context);

            if (!context.Response.HasStarted)
                throw;
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, ServiceException? exception = null)
    {
        HttpStatusCode statusCode = exception is not null ? exception.StatusCode : HttpStatusCode.InternalServerError;
        string message = exception is not null ? exception.Message : APIMessage.Error_GenericError;

        var errorResponse = new
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        string result = JsonSerializer.Serialize(errorResponse);

        await context.Response.WriteAsync(result);
    }
}
