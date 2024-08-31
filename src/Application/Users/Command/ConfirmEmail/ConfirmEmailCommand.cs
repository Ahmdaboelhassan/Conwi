using Application.DTO.Request;
using MediatR;

namespace Application.Users.Command.ConfirmEmail;

public record ConfirmEmailCommand(string email, string token) : IRequest<bool>
{
}
