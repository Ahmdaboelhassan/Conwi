using MediatR;

namespace Application.Messages.Command.SendMessage
{
    public record SendMessageCommand(DTO.Request.SendMessage Message) : IRequest<bool>;
}
