using Application.IRepository;
using Domain.Entity;
using MediatR;

namespace Application.Post.Commands.LikePost;

public class LikePostCommandHandler : IRequestHandler<LikePostCommand, bool>
{
    private readonly IUnitOfWork _uow;
    public LikePostCommandHandler(IUnitOfWork uow)
       => _uow = uow;
    
    public async Task<bool> Handle(LikePostCommand request, CancellationToken cancellationToken)
    {
        var userId = request.userId;
        var postId = request.postId;

        var user = await _uow.Users.Get(u => u.Id == userId);
        if (user is null) return false;

        var post = await _uow.Posts.Get(p => p.id == postId);
        if (post is null) return false;

        var userLike = await _uow.UserLike.Get(l => l.UserId == userId && l.PostId == postId);

        if (userLike == null)
        {
            post.Likes++;
            _uow.Posts.Update(post);
            await _uow.UserLike
                .AddAsync(new UserLike
                {
                    PostId = postId,
                    UserId = userId,
                });

        }
        else
        {
            post.Likes--;
            _uow.Posts.Update(post);
            _uow.UserLike.Delete(userLike);

        }
         _uow.SaveChanges();

        return true;

    }
}
