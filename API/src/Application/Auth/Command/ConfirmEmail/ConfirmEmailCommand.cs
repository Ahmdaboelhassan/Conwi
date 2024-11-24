using Application.DTO.Request;
using MediatR;

namespace Application.Auth.Command.ConfirmEmail;

public record ConfirmEmailCommand(string email, string token) : IRequest<bool>
{
}
