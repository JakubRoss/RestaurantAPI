using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _StopWatch;
        private ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _StopWatch = new Stopwatch();
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _StopWatch.Start();
            await next.Invoke(context);
            _StopWatch.Stop();
            var time =_StopWatch.ElapsedMilliseconds;
            if (time > 4000) 
            {
                var message = $"Request [{context.Request.Method}] at [{context.Request.Path}] took {time}ms";
                _logger.LogInformation(message);
            }
        }
    }
}