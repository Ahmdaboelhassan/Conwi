using Application.DTO.Response;
using Domain.Entity;
using Domain.IRepository;
using MediatR;

namespace Application.Messages.Queries.GetAllChats
{
    public class GetAllChatQueryHandler : IRequestHandler<GetAllChatQuery, IEnumerable<Chat>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllChatQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Chat>> Handle(GetAllChatQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = request.userId;

            var sendedMessages = await _unitOfWork.Messages.GetAll(m =>
                m.SenderId.Equals(currentUserId) || m.RevieverId.Equals(currentUserId));

            var userChatIds = sendedMessages.Select(m => (m.SenderId == currentUserId) ? m.RevieverId : m.SenderId).ToHashSet();
            
            var usersData = await _unitOfWork.Users.SelectAll(u => userChatIds.Contains(u.Id) , u => new { u.UserName , u.PhotoURL , u.FirstName , u.LastName , u.Id });

            var chats = usersData.Select(user =>
            {
                var lastMessage = sendedMessages
                .Last(m => (m.RevieverId == user.Id && m.SenderId == currentUserId) || (m.RevieverId == currentUserId && m.SenderId == user.Id));

                return new Chat()
                {
                    userId = user.Id,
                    lastMessage = lastMessage.Content,
                    lastMessageTime = lastMessage.SendTime,
                    lastMessageId = lastMessage.Id,
                    lastMessageRead = lastMessage.RevieverId != currentUserId || lastMessage.IsReaded,
                    userName = user.UserName,
                    userPhoto = user.PhotoURL,
                    UserFirstName = user.FirstName,
                    userLastName = user.LastName,
                };
            });

            return chats.OrderByDescending(ch => ch.lastMessageTime);
        }
    }
}
