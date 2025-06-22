using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;

namespace EmployeeAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(IWebHostEnvironment env, ILogger<CustomExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            // Write exception details to a file
            string filePath = Path.Combine(_env.ContentRootPath, "Logs", "exceptions.txt");
            string? directoryPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"Exception occurred at: {DateTime.Now}");
                writer.WriteLine($"Exception message: {context.Exception.Message}");
                writer.WriteLine($"Stack trace: {context.Exception.StackTrace}");
                writer.WriteLine(new string('-', 50));
            }

            // Set the result to ExceptionResult
            var result = new ObjectResult(new
            {
                error = context.Exception.Message,
                stackTrace = context.Exception.StackTrace
            })
            {
                StatusCode = 500
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}