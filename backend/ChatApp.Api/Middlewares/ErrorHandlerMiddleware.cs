using ChatApp.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ChatApp.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ErrorHandlerMiddleware> logger;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ErrorHandlerMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger,
            IWebHostEnvironment hostingEnvironment)
        {
            this.next = next;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await next(context);
            }
            catch (NotFoundException e)
            {
                logger.LogError(e, "Unhandled exception caught.");

                await WriteAsJsonAsync(
                    context,
                    (int)HttpStatusCode.NotFound,
                    new ErrorDto
                    {
                        Message = e.Message,
                        StackTrace = e.StackTrace
                    });
            }
            catch (Exception e)
            {
                logger.LogError(e, "Unhandled exception caught.");

                await WriteAsJsonAsync(
                    context,
                    (int)HttpStatusCode.InternalServerError,
                    new ErrorDto
                    {
                        StackTrace = e.StackTrace
                    });
            }

        }

        private Task WriteAsJsonAsync(HttpContext context, int statusCode, ErrorDto payload)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var json = hostingEnvironment.IsDevelopment()
                ? string.IsNullOrWhiteSpace(payload.Message)
                    ? SerializeObject(new { payload.StackTrace })
                    : SerializeObject(payload)
                : string.IsNullOrWhiteSpace(payload.Message)
                    ? ""
                    : SerializeObject(new { payload.Message });

            return context.Response.WriteAsync(json);
        }

        private string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        class ErrorDto
        {
            public string Message { get; set; }

            public string StackTrace { get; set; }
        }
    }
}
