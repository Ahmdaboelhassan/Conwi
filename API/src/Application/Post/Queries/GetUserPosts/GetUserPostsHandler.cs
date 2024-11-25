using Application.DTO.Response;
using Application.IRepository;
using Domain.Entity;
using MediatR;

namespace Application.Post.Queries.GetUserPosts;
public class GetUserPostsHandler : IRequestHandler<GetUserPostsQuery, IEnumerable<ReadPost>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetUserPostsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ReadPost>> Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await _unitOfWork.Posts.GetAll(p => p.UserPostedId == request.userId, "UserPosted");

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
        }).OrderByDescending(p => p.time);
    }
}
