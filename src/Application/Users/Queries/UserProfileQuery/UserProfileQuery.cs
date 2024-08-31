using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.UserProfileQuery;

public record UserProfileQuery(string userEmail) : IRequest<UserProfile?>;
