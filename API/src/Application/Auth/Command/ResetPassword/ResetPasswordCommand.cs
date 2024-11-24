using MediatR;

namespace Application.Auth.Command.ResetPassword
{
    public record ResetPasswordCommand(string email, string token, string newPassword) : IRequest<bool> { }
}
