using Application.DTO.Response;
using MediatR;

namespace Application.Auth.Command.Register;

public record RegisterCommand(DTO.Request.Register Model) : IRequest<AuthResponse> { }
