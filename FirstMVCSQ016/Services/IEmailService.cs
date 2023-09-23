namespace FirstMVCSQ016.Services
{
    public interface IEmailService
    {
        Task<bool> SendAsync(string recipientEmail, string subject, string body);
    }
}
