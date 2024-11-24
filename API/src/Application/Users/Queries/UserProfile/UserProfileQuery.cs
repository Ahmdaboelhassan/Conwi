using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.UserProfileQuery;

public record UserProfileQuery(string id , string? userId) : IRequest<UserProfile?>;
