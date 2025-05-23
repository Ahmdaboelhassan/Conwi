﻿using Application.DTO.Request;
using MediatR;

namespace Application.Auth.Queries.GetPasswordResetToken;

public record PasswordResetTokenQuery(string email) : IRequest<ConfirmationResponse>
{ }
