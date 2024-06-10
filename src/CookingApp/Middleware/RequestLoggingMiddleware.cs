using System.Data;
using System.Data.SqlClient;
using Dapper;
using CookingApp.Models;
using System.Data.SqlTypes;

namespace CookingApp.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string connectionString;
        private readonly bool loggingEnabled;

        public RequestLoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            next = next;
            connectionString = configuration.GetConnectionString("MsSqlServer");
            loggingEnabled = configuration.GetValue<bool>("Logging1:LogRequests:Enabled");
        }

        public async Task Invoke(HttpContext context)
        {
            if (!loggingEnabled)
            {
                await next(context);
                return;
            }

            var request = context.Request;
            var response = context.Response;

            request.EnableBuffering();

            var requestLog = new RequestLog
            {
                Url = request.Path,
                RequestBody = await ReadRequestBodyAsync(request),
                CreationDate = DateTime.UtcNow,
                HttpMethod = request.Method
            };

            var originalResponseBodyStream = response.Body;
            using var responseBodyStream = new MemoryStream();
            response.Body = responseBodyStream;

            try
            {
                await LogRequestAsync(requestLog);

                await next(context);
                response.Body.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
                requestLog.ResponseBody = await ReadResponseBodyAsync(response.Body);
                requestLog.StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                requestLog.StatusCode = 500;
                throw;
            }
            finally
            {
                requestLog.EndDate = DateTime.UtcNow;

                response.Body = originalResponseBodyStream;
            }
        }


        private async Task<string> ReadRequestBodyAsync(HttpRequest request)
        {
            request.Body.Position = 0;
            using var reader = new StreamReader(request.Body, leaveOpen: true);
            var content = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            return content;
        }

        private async Task<string> ReadResponseBodyAsync(Stream responseBody)
        {
            using var reader = new StreamReader(responseBody);
            var content = await reader.ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);
            return content;
        }

        private async Task LogRequestAsync(RequestLog logEntry)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var query = @"insert into RequestLogs (Url, RequestBody, ResponseBody, CreationDate, EndDate, StatusCode, HttpMethod)
                            values (@Url, @RequestBody, @ResponseBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)";

                logEntry.CreationDate = EnsureValidDateTime(logEntry.CreationDate);
                logEntry.EndDate = EnsureValidDateTime(logEntry.EndDate);
                
                await db.ExecuteAsync(query, logEntry);
            }
        }

        private DateTime EnsureValidDateTime(DateTime dateTime)
        {
            if (dateTime < SqlDateTime.MinValue.Value)
            {
                return SqlDateTime.MinValue.Value;
            }
            else if (dateTime > SqlDateTime.MaxValue.Value)
            {
                return SqlDateTime.MaxValue.Value;
            }
            return dateTime;
        }

    }
}
