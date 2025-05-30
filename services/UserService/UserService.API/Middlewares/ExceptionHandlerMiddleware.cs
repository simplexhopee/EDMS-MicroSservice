using System.Text;
using System.Text.Json;

namespace UserService.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                string trace = context.TraceIdentifier;
                _logger.LogError(ex, $"Unhandled exception {trace}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;
                var body = new
                {
                    Status = 500,
                    Message = "An unhandled exception occurred",
                    TraceId= trace,
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(body));

            }
        }
    }
}
