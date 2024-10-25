using Application.DTO.Request;
using MediatR;

namespace Application.Users.Queries.GetPasswordResetToken;

public record PasswordResetTokenQuery(string email) : IRequest<ConfirmationResponse>
{}
