using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.GetUsers;
public record GetUsersQuery(string? criteria , string userId) : IRequest<IEnumerable<UserCard>>{}