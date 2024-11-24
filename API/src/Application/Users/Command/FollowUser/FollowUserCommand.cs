using Application.DTO.Request;
using MediatR;

namespace Application.Users.Command.FollowUser;
public record FollowUserCommand(FollowUserRequest request) : IRequest;
