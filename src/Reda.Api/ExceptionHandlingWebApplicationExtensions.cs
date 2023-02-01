using System.Net;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;

using Reda.Application.Exceptions;

namespace Reda.Api;

public static class ExceptionHandlingWebApplicationExtensions
{
    public static WebApplication UseExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                var exceptionHandlerPathFeature = context.Features.GetRequiredFeature<IExceptionHandlerPathFeature>();
                var error = exceptionHandlerPathFeature.Error;
                var apiError = error switch
                {
                    OrderNotFoundException => new ApiError($"Not found {error.Message}", HttpStatusCode.NotFound),
                    OrderAlreadyExistsException or InvalidProductTypeException => 
                        new ApiError($"Bad request {error.Message}", HttpStatusCode.BadRequest),
                    _ => new ApiError("Internal server error", HttpStatusCode.InternalServerError)
                };

                context.Response.StatusCode = (int)apiError.HttpStatusCode;
                await context.Response.WriteAsJsonAsync(apiError);

                logger.LogError(exceptionHandlerPathFeature.Error, "Error when executing Action");
            });
        });

        return app;
    }
}

public record ApiError(string Message, HttpStatusCode HttpStatusCode);