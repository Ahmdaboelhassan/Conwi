using AutoMapper;
using Domain.IRepository;
using MediatR;

namespace Application.Messages.Command.DeleteMessage;

public class DeleteMessageCommandHandler : MessageCommandBaseHandler<DeleteMessageCommand, bool>
{
    public DeleteMessageCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async override Task<bool> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _unitOfWork.Messages.Get(m => m.Id == request.messageId);

        if (message == null || !message.SenderId.Equals(request.userId))
            return false;

        message.IsDeleted = true;
        _unitOfWork.Messages.Update(message);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}

