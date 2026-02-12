namespace PiggyBank.Reposiroty.Entity.Logger
{
    public class ErrorLog
    {
        public long Id { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        public int StatusCode { get; set; }

        public string TraceId { get; set; } = "";
        public string Method { get; set; } = "";
        public string Path { get; set; } = "";
        public string? QueryString { get; set; }

        public string? UserId { get; set; }      // if you have auth
        public string? UserName { get; set; }    // if you have auth
        public string? RemoteIp { get; set; }

        public string ExceptionType { get; set; } = "";
        public string Message { get; set; } = "";
        public string? StackTrace { get; set; }
        public string? InnerMessage { get; set; }
    }
}
