using Application.DTO.Request;
using Application.DTO.Response;
using MediatR;

namespace Application.Messages.Queries.GetPrivateChat;
public record GetPrivateChatQuery (string userId , string contactId) : IRequest<PrivateChat>;
