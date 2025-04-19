using Application.DTO.Response;
using Domain.IRepository;
using MediatR;

namespace Application.Notifications.Queries
{
    public record GetUserNotificationQuery(string userId) : IRequest<IEnumerable<NotificationDTO>>;

    public class GetUserNotificationQueryHandler : IRequestHandler<GetUserNotificationQuery, IEnumerable<NotificationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserNotificationQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<NotificationDTO>> Handle(GetUserNotificationQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.Notification.SelectAll(a => a.DestUser == request.userId , 
                notification => new NotificationDTO
                {
                    Id = notification.Id,
                    Title = notification.Title,
                    Message = notification.Message,
                    Time = notification.Time,
                    IsRead = notification.IsRead,
                    Photo = notification.Photo,
                    Type = (byte)notification.Type
                }))
                .OrderByDescending(n => n.Time)
                .Take(20);
        }
    }
    
}
