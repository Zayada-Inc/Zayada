namespace Domain.Interfaces
{
    public interface IEmailService
    {
        string EmailMessage(string text);
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}