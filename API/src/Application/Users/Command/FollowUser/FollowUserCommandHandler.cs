using Application.DTO.Response;
using Domain.Entity;
using Domain.IRepository;
using MediatR;

namespace Application.Users.Command.FollowUser;
internal class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public FollowUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(FollowUserCommand request, CancellationToken cancellationToken)
    {
        var source = request.request.sourceId;
        var dist = request.request.destId;

        var sourceUser = await _unitOfWork.Users.Get(u => u.Id == source);   

        var userFollow = await _unitOfWork.UserFollow.Get(uf => uf.SourceUserId == source && uf.DistinationUserId == dist);

        if (userFollow == null)
        {
            await _unitOfWork.UserFollow.AddAsync(
                new UserFollow
                {
                    DistinationUserId = request.request.destId,
                    SourceUserId = request.request.sourceId,
                });

            // send notification 
            var notification = new Notification
            {
                DestUser = dist,
                SourceUser = source,
                Title = "New Follower",
                Photo = sourceUser.PhotoURL,
                Message = $"{sourceUser.UserName} started following you",
                Time = DateTime.Now,
                Type = (byte)NotificationTypes.Follow
            };

            await _unitOfWork.Notification.AddAsync(notification);
        }
        else {
            _unitOfWork.UserFollow.Delete(userFollow);
          }

        _unitOfWork.SaveChanges();

    }
}
