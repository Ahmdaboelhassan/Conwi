using MediatR;

namespace Application.Post.Commands.DeletePost;

public record DeletePostCommand(int postId , string userId) : IRequest<bool>;

