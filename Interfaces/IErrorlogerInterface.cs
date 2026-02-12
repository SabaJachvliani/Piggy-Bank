namespace PiggyBank.Interfaces
{
    public interface IErrorlogerInterface
    {
        Task LogAsync(Exception ex, HttpContext ctx, int statusCode = 500, string? note = null);
    }
}
