using Application.IRepository;
using MediatR;

namespace Application.Post.Commands.DeletePost;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IUnitOfWork _uow;

    public DeletePostCommandHandler(IUnitOfWork uow)
       => _uow = uow;

    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _uow.Posts.Get(p => p.id == request.postId);

        if (post != null &&  post.UserPostedId == request.userId)
        {
            _uow.Posts.Delete(post);
            _uow.SaveChanges();
            return true;
        }
        return false;
    }
}
