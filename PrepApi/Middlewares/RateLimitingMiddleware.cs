
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Caching.Memory;

namespace PrepApi.Middlewares
{
    public class RateLimitingMiddleware : IMiddleware
    {
        private static int threshold = 10;
        private static TimeSpan rateLimitingTimeInMinutes = TimeSpan.FromMinutes(2);
        private IMemoryCache _memory;
        public RateLimitingMiddleware(IMemoryCache memory)
        {
            _memory = memory;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var ip = context.Connection.RemoteIpAddress ? .ToString();
            if (ip == null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return;
            }
            var requestTime = _memory.GetOrCreate(ip, x =>
            {
                x.AbsoluteExpirationRelativeToNow = rateLimitingTimeInMinutes;
                return new List<DateTime>();
            });

            //it will remove all the entries that are behind my rateLimit time
            requestTime.RemoveAll(t => t < DateTime.Now - rateLimitingTimeInMinutes);
        
            if(requestTime.Count >= threshold)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync($"You can only send {threshold} request in {rateLimitingTimeInMinutes} mins");
                return;
            }
            requestTime.Add(DateTime.Now);

            await next(context);
        }
    }
}
