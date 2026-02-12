using Microsoft.AspNetCore.Http;
using PiggyBank.Interfaces;
using PiggyBank.Reposiroty.Database;
using PiggyBank.Reposiroty.Entity.Logger;

namespace PiggyBank.Servises.LoggerService
{
    public class DbErrorLogger : IErrorlogerInterface
    {
        private readonly PiggyBankDbContext _db;

        public DbErrorLogger(PiggyBankDbContext db)
        {
            _db = db;
        }

        public async Task LogAsync(Exception ex, HttpContext ctx, int statusCode = 500, string? note = null)
        {
            var userId = ctx.User?.FindFirst("sub")?.Value
                      ?? ctx.User?.FindFirst("id")?.Value;

            var userName = ctx.User?.Identity?.Name;

            var log = new ErrorLog
            {
                CreatedUtc = DateTime.UtcNow,
                StatusCode = statusCode,
                TraceId = ctx.TraceIdentifier,
                Method = ctx.Request.Method,
                Path = ctx.Request.Path.ToString(),
                QueryString = ctx.Request.QueryString.ToString(),
                RemoteIp = ctx.Connection.RemoteIpAddress?.ToString(),
                UserId = userId,
                UserName = userName,
                ExceptionType = ex.GetType().FullName ?? "Exception",
                Message = note is null ? ex.Message : $"{note} | {ex.Message}",
                StackTrace = ex.StackTrace,
                InnerMessage = ex.InnerException?.Message
            };

            _db.ErrorLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
