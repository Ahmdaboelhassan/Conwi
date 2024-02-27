using API.Models;

namespace API.Services.IServices
{
    public interface IAuthService
    {
        public Task<AuthResponse> RegisterAsync(Register model);
        public Task<AuthResponse> LoginAsync (LogIn model);
        public Task<ConfirmationResponse> GetPasswordResetToken(string email);
        public Task<ConfirmationResponse> GetEmailConfirmationToken(string email);
        public Task<bool> ConfrimEmailAsync(string email , string token);
        public Task<bool> ResetPasswordAsync (string email, string token, string newPassword);

    }
}