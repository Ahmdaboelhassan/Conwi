using MediatR;

namespace Application.Users.Command.ResetPassword
{
    public record ResetPasswordCommand(string email, string token, string newPassword) : IRequest<bool> {}
}
