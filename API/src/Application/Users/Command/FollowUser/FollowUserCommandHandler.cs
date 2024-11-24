using Application.IRepository;
using Domain.Entity;
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

        var userFollow = await _unitOfWork.UserFollow.Get(uf => uf.SourceUserId == source && uf.DistinationUserId == dist);

        if (userFollow == null)
            await _unitOfWork.UserFollow.AddAsync(
                new UserFollow
                {
                    DistinationUserId = request.request.destId,
                    SourceUserId = request.request.sourceId,
                });

        else  _unitOfWork.UserFollow.Delete(userFollow);

        _unitOfWork.SaveChanges();

    }
}
