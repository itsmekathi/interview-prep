public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Log Request Details
        System.Console.WriteLine($"Incoming Request: {context.Request.Method} {context.Request.Path}");

        // call the next middleware in the pipeline
        await _next(context);

        // Log Response Details
        System.Console.WriteLine($"Outgoing Response: {context.Response.StatusCode}");
    }
}