using Application.DTO.Response;
using Application.IRepository;
using Domain.Entity;
using MediatR;

namespace Application.Users.Queries.GetUserPosts;
public class GetUserPostsHandler : IRequestHandler<GetUserPostsQuery, IEnumerable<ReadPost>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetUserPostsHandler(IUnitOfWork unitOfWork)
    {
            _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ReadPost>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Posts.GetAll(p => p.UserPostedId == request.userId , "UserPosted");

        return posts.Select(p => new ReadPost{
            Id = p.id,
            userId = p.UserPostedId,
            userEmail = p.UserPosted.Email,
            username = p.UserPosted.UserName,
            userPhoto = p.UserPosted.PhotoURL,
            content = p.content,
            imgUrl = p.photoURL,
            Time = p.timePosted
        }).OrderByDescending(p => p.Time);
    }
}
