namespace Application.IServices
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(string sendTo, string subject, string body);
    }
}