using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.GetUserPosts;

public record GetUserPostsQuery(string userId) : IRequest<IEnumerable<ReadPost>>;

