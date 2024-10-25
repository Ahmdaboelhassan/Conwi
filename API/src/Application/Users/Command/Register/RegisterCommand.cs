
using Application.DTO.Response;
using MediatR;

namespace Application.Users.Command.Register;

public record RegisterCommand(Application.DTO.Request.Register Model) : IRequest<AuthResponse> {}
