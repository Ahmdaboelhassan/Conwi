using MediatR;

namespace Application.Post.Commands.CreatePost;

public record CreatePostCommand(DTO.Request.CreatePost PostModel) : IRequest<bool> { }
