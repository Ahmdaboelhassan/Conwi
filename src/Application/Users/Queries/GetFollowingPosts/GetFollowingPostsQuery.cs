using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO.Response;
using MediatR;

namespace Application.Users.Queries.GetFollowingPosts
{
    public record GetFollowingPostsQuery(string userId) : IRequest<IEnumerable<ReadPost>>;
}