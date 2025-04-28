using GBMO.Teach.Application.Extensions;
using GBMO.Teach.Core.Configurations.Logging;
using GBMO.Teach.Core.Constants;
using System.Text;

namespace GBMO.Teach.API.Middlewares
{
    public class LoggingMiddeware 
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;

        public LoggingMiddeware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Path.Equals(PathContants.LoginPath))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    var infoLog = await CreateInformationLogAsync(context);
                    var originalBodyStream = context.Response.Body;
                    using (var responseBody = new MemoryStream())
                    {
                        context.Response.Body = responseBody;

                        await _next.Invoke(context);

                        infoLog.ResponseStatusCode = context.Response.StatusCode;
                        infoLog.ResponseBody = await ReadResponseBodyAsync(context.Response);

                        await responseBody.CopyToAsync(originalBodyStream);
                    }

                    _logger.LogInformation<InformationLogFormat>(infoLog);
                }
            }
            catch (Exception ex)
            {
                var errorLog = await CreateErrorLogAsync(context, ex.Message, ex.StackTrace);
                _logger.LogError<ErrorLogFormat>(errorLog);
            }
        }


        private async Task<string> ReadRequestBodyAsync(HttpRequest httpRequest)
        {
            var requestContent = string.Empty;

            httpRequest.EnableBuffering();
            using (var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, true, 1024, true))
            {
                requestContent = await reader.ReadToEndAsync();
            }
            httpRequest.Body.Position = 0;

            return requestContent;
        }


        private async Task<string> ReadResponseBodyAsync(HttpResponse httpResponse)
        {
            httpResponse.Body.Seek(0, SeekOrigin.Begin);
            var responseContent = await new StreamReader(httpResponse.Body).ReadToEndAsync();
            httpResponse.Body.Seek(0, SeekOrigin.Begin);
            
            return responseContent;
        }


        private async Task<InformationLogFormat> CreateInformationLogAsync(HttpContext httpContext)
        {
            var informationLog = new InformationLogFormat()
            {
                HttpMethod = httpContext.Request.Method,
                RequestPath = httpContext.Request.Path,
                RequestBody = await ReadRequestBodyAsync(httpContext.Request)
            };

            return informationLog;
        }

        private async Task<ErrorLogFormat> CreateErrorLogAsync(HttpContext httpContext, string exceptionMessage, string exceptionStackTrace)
        {
            var errorLog = new ErrorLogFormat()
            {
                HttpMethod = httpContext.Request.Method,
                RequestPath = httpContext.Request.Path,
                RequestBody = await ReadRequestBodyAsync(httpContext.Request),
                ExceptionMessage = exceptionMessage,
                ExceptionStackTrace = exceptionStackTrace
            };

            return errorLog;
        }
    }
}
