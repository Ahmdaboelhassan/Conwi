using MediatR;

namespace Application.Messages.Queries.GetUnreadMessages;

public record GetUnReadMessageQuery : IRequest<int>;
