using MediatR;

namespace Application.Post.Commands.LikePost;

public record  LikePostCommand (int postId , string userId) : IRequest<bool>;

