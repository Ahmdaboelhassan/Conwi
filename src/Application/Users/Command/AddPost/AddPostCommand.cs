using Application.DTO.Request;
using MediatR;

namespace Application.Users.Command.AddPost;

public record AddPostCommand(PostModel PostModel) : IRequest<bool> {}
