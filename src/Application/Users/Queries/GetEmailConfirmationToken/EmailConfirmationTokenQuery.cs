using Application.DTO.Request;
using MediatR;

namespace Application.Users.Queries.GetEmailConfirmationToken;

public record EmailConfirmationTokenQuery(string email) : IRequest<ConfirmationResponse>
{}
