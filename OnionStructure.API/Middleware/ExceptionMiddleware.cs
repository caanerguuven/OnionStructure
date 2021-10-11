using OnionStructure.API.ViewModels;
using OnionStructure.Contract.Utils.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace OnionStructure.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogService _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogService logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Error($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsync(new ErrorDetail()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message,
                //Source = ex.StackTrace
            }.ToString());
        }

    }
}
