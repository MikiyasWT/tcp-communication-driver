using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using TcpWebApi.Contracts;
using TcpWebApi.Entities;

namespace TcpWebApi.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILoggingService _logger;

        public GlobalExceptionHandlingMiddleware(ILoggingService logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[ERROR] An exception occurred: {Message}", ex.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var problem = new ErrorDetails()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An internal server error has occurred."
                };

                string json = JsonSerializer.Serialize(problem);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
