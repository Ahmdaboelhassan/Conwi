using AutoMapper;
using Domain.Entity;
using Domain.IRepository;

namespace Application.Messages.Command.SendMessage
{
    public class SendMessageCommandHandler : MessageCommandBaseHandler<SendMessageCommand, bool>
    {
        public SendMessageCommandHandler(IUnitOfWork uow) : base(uow) { }
        public async override Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var model = request.Message;

            if ( string.IsNullOrEmpty(model.SenderId) 
              || string.IsNullOrEmpty(model.RevieverId) 
              || string.IsNullOrEmpty(model.Content))
                return false;

            var users = await _unitOfWork.Users
                .SelectAll(u => u.Id.Equals(model.SenderId) || u.Id.Equals(model.RevieverId), user => user.Id);

            if (users.Count() != 2)
                return false;

            model.SendTime = DateTime.Now;

            Message newMessage = model;

           await _unitOfWork.Messages.AddAsync(newMessage);
           await _unitOfWork.SaveChangesAsync();

           return true;
        }
    }
}
