using Application.DTO.Request;
using Application.DTO.Response;
using MediatR;

namespace Application.Users.Command.Login
{
    public record UserLoginCommand (LogIn model): IRequest<AuthResponse>{}
}
