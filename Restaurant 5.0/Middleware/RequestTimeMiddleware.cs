using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Restaurant_5._0.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopwatch;
        private readonly ILogger _logger;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            System.Int64 elapsed = _stopwatch.ElapsedMilliseconds;
            if(elapsed/1000>4)
            {
                string message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsed} ms";
                _logger.LogInformation(message);
            }
        }
    }
}
