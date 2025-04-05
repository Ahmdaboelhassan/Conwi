
using MediatR;

namespace Application.Messages.Command.DeleteMessage;

public record DeleteMessageCommand(int messageId, string userId) : IRequest<bool>;
