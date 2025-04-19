using Domain.IRepository;
using MediatR;

namespace Application.Notifications.Command
{
    public record ReadNotificationCommand(int notificationId) : IRequest<bool>;
    public class ReadNotificationCommandHandler : IRequestHandler<ReadNotificationCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadNotificationCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = await _unitOfWork.Notification.Get(x => x.Id == request.notificationId);
            if (notification is null)
                return false;

            notification.IsRead = true;
            _unitOfWork.Notification.Update(notification);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
