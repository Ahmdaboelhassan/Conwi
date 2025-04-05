using Application.DTO.Response;
using MediatR;

namespace Application.Messages.Queries.GetAllChats;

public record GetAllChatQuery(string userId):IRequest<IEnumerable<Chat>>;


