using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.GetNonFollowingUsers ;
public record ExploreUsersQuery (string userId) : IRequest<IEnumerable<UserCard>>{}
