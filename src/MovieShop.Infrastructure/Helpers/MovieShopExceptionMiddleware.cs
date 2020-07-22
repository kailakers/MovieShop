using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieShop.Core.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MovieShop.Infrastructure.Helpers
{
    public class MovieShopExceptionMiddleware
    {
        private readonly ILogger<MovieShopExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in ExceptionMiddleware: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            _logger.LogInformation($"Request completed with status code: {context.Response.StatusCode} ");
            _logger.LogError($"Something went wrong: {exception}");
            var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();
            string result;
            var matchText = "JSON";

            bool requiresJsonResponse = context.Request
                .GetTypedHeaders()
                .Accept
                .Any(t => t.Suffix.Value?.ToUpper() == matchText
                          || t.SubTypeWithoutSuffix.Value?.ToUpper() == matchText);

            if (env.IsDevelopment())
            {
                var errorDetails = new ErrorResponseModel
                {
                    ExceptionMessage = exception.Message,
                    ExceptionStackTrace = exception.StackTrace,
                    InnerExceptionMessage = exception.InnerException?.Message
                };
                result = JsonSerializer.Serialize(new {errors = errorDetails});

                if (!requiresJsonResponse)
                {
                    context.Items.Add("ErrorDetails", errorDetails);
                }
            }
            else
            {
                result = JsonSerializer.Serialize(new {errors = exception.Message});
            }

            switch (exception)
            {
                case BadRequestException _:
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    break;

                case NotFoundException _:
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException _:
                    context.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    break;

                case ConflictException ex:
                    context.Response.StatusCode = (int) HttpStatusCode.Conflict;
                    break;

                case Exception e:
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    break;
            }

            if (requiresJsonResponse)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
            else
            {
                context.Items["test"] = "testdata";
                context.Response.Redirect("/Home/Error");
            }
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieShopExceptionMiddleware>();
        }
    }
}