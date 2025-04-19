using Application.DTO.Request;
using Application.DTO.Response;
using Domain.Entity;
using Domain.IRepository;
using MediatR;
using System.Linq.Expressions;

namespace Application.Messages.Queries.GetPrivateChat
{
    public class GetPrivateChatQueryHandler : IRequestHandler<GetPrivateChatQuery, PrivateChat>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPrivateChatQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<PrivateChat> Handle(GetPrivateChatQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.Get(u => u.Id == request.contactId);
            if (user is null)
                return new PrivateChat();

            Expression<Func<Message, bool>> predicate =
                m => (m.SenderId == request.userId && m.RevieverId == request.contactId)
                || (m.SenderId == request.contactId && m.RevieverId == request.userId);

            var messages = await _unitOfWork.Messages.GetAll(predicate);

            var receivedMessages = messages.Where(m => !m.IsReaded && m.RevieverId == request.userId);

            if (receivedMessages.Any())
            {
                foreach (var msg in receivedMessages)
                {
                    msg.IsReaded = true;
                    _unitOfWork.Messages.Update(msg);
                }
                await _unitOfWork.SaveChangesAsync();
            }

            var chatMessages = messages.Select(m => new ChatMessage
            {
                Id = m.Id,
                RevieverId = m.RevieverId,
                SenderId = m.SenderId,
                Content = m.Content,
                IsDeleted = m.IsDeleted,
                IsReaded = m.IsReaded,
                IsDelivered = m.IsDelivered,
                SendTime = m.SendTime,
                DeliverTime = m.DeliverTime
            });

            return new PrivateChat
            {
                userId = user.Id,
                username = user.FirstName,
                userPhoto = user.PhotoURL,
                firstName = user.FirstName,
                lastName = user.LastName,
                messages = chatMessages,
            };
        }
    }
}
