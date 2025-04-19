using Application.Users.Extension;
using Domain.IRepository;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Messages.Queries.GetUnreadMessages
{
    class GetUnreadMessageHandler : IRequestHandler<GetUnReadMessageQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public GetUnreadMessageHandler(IUnitOfWork unitOfWork , IHttpContextAccessor context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public async Task<int> Handle(GetUnReadMessageQuery request, CancellationToken cancellationToken)
        {
            var userId = _context.HttpContext.User.GetUserId();
            var messages = await _unitOfWork.Messages.GetAll(m => m.RevieverId == userId && !m.IsReaded);
            return messages.GroupBy(x => x.SenderId).Count();
        }
    }
}
