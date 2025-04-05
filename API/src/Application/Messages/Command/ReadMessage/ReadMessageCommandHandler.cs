using Application.IRepository;
using AutoMapper;

namespace Application.Messages.Command.ReadMessage;

public class ReadMessageCommandHandler : MessageCommandBaseHandler<ReadMessageCommand, bool>
{
    public ReadMessageCommandHandler(IUnitOfWork unitOfWork) 
        : base(unitOfWork){}

    public async override Task<bool> Handle(ReadMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _unitOfWork.Messages.Get(m => m.Id == request.messageId);

        if (message == null || message.RevieverId != request.userId)
            return false;

        message.IsReaded = true;
        message.IsDelivered = true;
        message.DeliverTime = DateTime.UtcNow;
        _unitOfWork.Messages.Update(message);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
