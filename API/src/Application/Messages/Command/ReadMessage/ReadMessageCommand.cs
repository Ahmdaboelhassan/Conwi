using MediatR;

namespace Application.Messages.Command.ReadMessage;

public record ReadMessageCommand(int messageId , string userId) :  IRequest<bool>;

