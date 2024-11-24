using Application.DTO.Response;
using MediatR;

namespace Application.Post.Queries.GetUserPosts;

public record GetUserPostsQuery(string userId) : IRequest<IEnumerable<ReadPost>>;

