﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Library;
using System.ComponentModel.DataAnnotations;

namespace Order.API.Middlewares;

public static class GlobalExceptionHandler
{
    public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                var statusCode = exceptionFeature.Error switch
                {
                    ValidationException => StatusCodes.Status422UnprocessableEntity,
                    _ => 500
                };
                context.Response.StatusCode = statusCode;
                var response = ApiResponse<NoContent>.Fail(exceptionFeature.Error.Message, statusCode);
                await context.Response.WriteAsJsonAsync(response);
            });
        });
    }
}
