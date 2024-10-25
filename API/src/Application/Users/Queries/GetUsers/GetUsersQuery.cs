using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.GetUsers;
public record GetUsersQuery(string criteria) : IRequest<IEnumerable<UserCard>>{}