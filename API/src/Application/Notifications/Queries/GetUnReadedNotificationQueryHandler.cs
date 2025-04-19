using Application.Users.Extension;
using Domain.IRepository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Notifications.Queries
{
    public record GetUnReadNotificationQuery : IRequest<int>;
    class GetUnReadedNotificationQueryHandler : IRequestHandler<GetUnReadNotificationQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public GetUnReadedNotificationQueryHandler(IUnitOfWork unitOfWork, IHttpContextAccessor context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(GetUnReadNotificationQuery request, CancellationToken cancellationToken)
        {
            var userId = _context.HttpContext.User.GetUserId();
            var messages = await _unitOfWork.Notification.GetAll(m => m.DestUser == userId && !m.IsRead);
            return messages.Count();
        }
    }
    
}
