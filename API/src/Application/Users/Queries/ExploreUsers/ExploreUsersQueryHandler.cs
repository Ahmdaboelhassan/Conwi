using Application.DTO.Response;
using Application.IRepository;
using MediatR;

namespace Application.Users.Queries.GetNonFollowingUsers;
public class ExploreUsersQueryHandler : IRequestHandler<ExploreUsersQuery, IEnumerable<UserCard>>
{
    private readonly IUnitOfWork _unitOfWork;
    public ExploreUsersQueryHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
    public async Task<IEnumerable<UserCard>> Handle(ExploreUsersQuery request, CancellationToken cancellationToken)
    {
        //var followingIds = await _unitOfWork.UserFollow.SelectAll(uf => uf.SourceUserId == request.userId , (uf) => uf.DistinationUserId);
       // var set = new HashSet<string>(followingIds);
        var currentUserFollowers = await _unitOfWork.UserFollow.GetAll(f => f.SourceUserId == request.userId);
        var currentUserFollowersSet = new HashSet<string>(currentUserFollowers.Select(x => x.DistinationUserId));


        return await _unitOfWork.Users
            .SelectAll(user =>/* !set.Contains(user.Id) &&*/ user.Id != request.userId
           , (user) => new UserCard
           {
               userId = user.Id,
               firstName = user.FirstName,
               lastName = user.LastName,
               photo = user.PhotoURL,
               UserName = user.UserName,
               following = currentUserFollowersSet.Contains(user.Id),
           });

        
    }
}
