using Application.DTO.Response;
using Application.IRepository;
using MediatR;

namespace Application.Users.Queries.GetFollowingPosts;
public class GetFollowingPostsQueryHandler : IRequestHandler<GetFollowingPostsQuery, IEnumerable<ReadPost>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetFollowingPostsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ReadPost>> Handle(GetFollowingPostsQuery request, CancellationToken cancellationToken)
    {
         var posts = await _unitOfWork.Posts.GetAll("UserPosted");

        return posts.Select(p => new ReadPost{
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
