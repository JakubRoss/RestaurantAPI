using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(ForbiddenException ForbiddenException)
            {
                context.Response.StatusCode = 403;
            }
            catch(BadRequestException BadRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(BadRequestException.Message);
            }
            catch(NotFoundException NotFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(NotFoundException.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("unknown error");
            }
        }
    }
}
