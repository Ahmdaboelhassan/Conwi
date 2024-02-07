namespace API.Services.IServices
{
    public interface IEmailService
    {
        public Task<bool> SendEmail(string sendTo , string subject,string body);
    }
}