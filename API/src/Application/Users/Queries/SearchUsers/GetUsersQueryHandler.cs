using System.Linq.Expressions;
using Application.DTO.Response;
using Application.IRepository;
using Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserCard>>
{
    private readonly IUnitOfWork _uow;

    public GetUsersQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<UserCard>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var currentUserFollowers = await _uow.UserFollow.GetAll(f => f.SourceUserId == request.userId);
        var currentUserFollowersSet = new HashSet<string>(currentUserFollowers.Select(x => x.DistinationUserId));

        string criteria = request.criteria;

        Expression<Func<AppUser, bool>> expression = u => request.userId != u.Id;

        if (!string.IsNullOrEmpty(criteria) && criteria.Length > 2)
        {
            criteria = criteria.ToLower();

            expression = u => 
             (u.UserName.ToLower().Contains(criteria)
              ||  u.Email.ToLower().StartsWith(criteria)
              || (u.FirstName+" "+ u.LastName).ToLower().StartsWith(criteria))
              && request.userId != u.Id;
        }

        var users = await _uow.Users.SelectAll(expression , 
            (u) => new UserCard
            {
                userId = u.Id,
                UserName = u.UserName,
                firstName = u.FirstName,
                lastName = u.LastName,
                photo = u.PhotoURL,
                following = currentUserFollowersSet.Contains(u.Id),
            });
        
        return users;
    }
}
