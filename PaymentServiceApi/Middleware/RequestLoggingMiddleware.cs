using System.Diagnostics;
using Serilog;

namespace PaymentServiceApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                await _next(context);
                stopwatch.Stop();
                var response = context.Response;

                Log.Information("Incoming Request: {Method} {Url} responded {statusCode} in {ElapsedSeconds} sec ", request.Method, request.Path, response.StatusCode, stopwatch.Elapsed.TotalSeconds);

                //Log.Information("Response: {statusCode} {ElapsedSeconds} sec", response.StatusCode, stopwatch.Elapsed.TotalSeconds);
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                Log.Error(ex, "Http {Method}{Path} failed in {ElapsedSeconds} sec", request.Method, request.Path, stopwatch.Elapsed.TotalSeconds);

                throw;
            }
        }
    }
}
