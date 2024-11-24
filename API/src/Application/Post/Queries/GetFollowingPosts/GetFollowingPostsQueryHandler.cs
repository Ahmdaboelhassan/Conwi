using Application.DTO.Response;
using Application.IRepository;
using Domain.Entity;
using MediatR;

namespace Application.Post.Queries.GetFollowingPosts;
public class GetFollowingPostsQueryHandler : IRequestHandler<GetFollowingPostsQuery, IEnumerable<ReadPost>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFollowingPostsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ReadPost>> Handle(GetFollowingPostsQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.userId))
            return Enumerable.Empty<ReadPost>();

        var FollowingUsers = await _unitOfWork
            .UserFollow.GetAll(uf => uf.SourceUserId == request.userId);

        // Get Ids Of Users That The User Id Has Sent Follow 
        var FollowingUsersIds = FollowingUsers.Select(uf => uf.DistinationUserId).ToList();

        var posts = await _unitOfWork.Posts
           .GetAll(p => FollowingUsersIds.Any(x => x == p.UserPostedId), "UserPosted");

        return posts.Select(p => new ReadPost
        {
            Id = p.id,
            userId = p.UserPostedId,
            userEmail = p.UserPosted.Email,
            username = p.UserPosted.UserName,
            userPhoto = p.UserPosted.PhotoURL,
            content = p.content,
            imgUrl = p.photoURL,
            Time = p.timePosted,
        }).OrderByDescending(p => p.Time);
    }
}
