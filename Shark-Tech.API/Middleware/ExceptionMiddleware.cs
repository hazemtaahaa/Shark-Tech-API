using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Shark_Tech.API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(30); // Cache duration for 30 seconds
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env, IMemoryCache memoryCache)
    {
        _next = next;
        _logger = logger;
        _env = env;
        _memoryCache = memoryCache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        { 
            if(IsRequestAllowed(context) == false)
            { 
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                context.Response.ContentType = "application/json";
                var response = new ApiException(context.Response.StatusCode, "Too many requests. Please try again later.");
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
                return;
            }
            await _next(context); // Proceed to the next middleware
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new ApiException(context.Response.StatusCode, ex.Message,ex.StackTrace);
            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);

        }
    }

    private bool IsRequestAllowed(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();  // Get the client's IP address
        var cacheKey = $"Rate:{ip}";                              // Unique cache key for each IP address
        var dateNow = DateTime.Now;                              // Get the current date and time

        // Check if the cache entry exists
        var (timesTamp, count) = _memoryCache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
            return (dateNow, 0);
        });


        // Check if the request is within the cache duration

        if (dateNow - timesTamp < _cacheDuration)
        {
          if(count >= 8)
            {
                return false; // Too many requests
            }
            else
            {
                _memoryCache.Set(cacheKey, (timesTamp, count + 1), _cacheDuration);
                return true; // Request allowed
            }
        }
        else
        {
            _memoryCache.Set(cacheKey, (timesTamp, count + 1), _cacheDuration);
        }
            return true; // Request allowed

    }

 
}
