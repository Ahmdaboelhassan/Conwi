using Application.DTO.Request;
using MediatR;

namespace Application.Users.Command.AddPost;

public record CreatePostCommand(CreatePost PostModel) : IRequest<bool> {}
