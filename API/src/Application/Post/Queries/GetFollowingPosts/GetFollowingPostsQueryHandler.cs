using Application.DTO.Response;
using Domain.Entity;
using Domain.IRepository;
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

        var userLiked = await _unitOfWork.UserLike.SelectAll(x => x.UserId == request.userId , p => p.PostId);

        return posts.Select(p => new ReadPost
        {
            id = p.id,
            userId = p.UserPostedId,
            userEmail = p.UserPosted.Email,
            username = p.UserPosted.UserName,
            userPhoto = p.UserPosted.PhotoURL,
            content = p.content,
            imgUrl = p.photoURL,
            time = p.timePosted,
            firstName = p.UserPosted.FirstName,
            lastName = p.UserPosted.LastName,
            likes = p.Likes,
            isLiked = userLiked.Any(pId => pId == p.id)

        }).OrderByDescending(p => p.time);
    }
}
