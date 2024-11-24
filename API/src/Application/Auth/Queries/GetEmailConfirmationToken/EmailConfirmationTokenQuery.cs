using Application.DTO.Request;
using MediatR;

namespace Application.Auth.Queries.GetEmailConfirmationToken;

public record EmailConfirmationTokenQuery(string email) : IRequest<ConfirmationResponse>
{ }
