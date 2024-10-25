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
        string criteria = request.criteria;

        Expression<Func<AppUser, bool>> expression = 
               u => u.UserName.ToLower().Contains(criteria.ToLower())
            || u.Email.ToLower().Contains(criteria.ToLower() )
            || u.FirstName.ToLower().Contains(criteria.ToLower() )
            || u.LastName.ToLower().Contains(criteria.ToLower() );

       return await _uow.Users
            .SelectAll(expression
            ,(u) => new UserCard{
                UserName = u.UserName,
                firstName = u.FirstName,
                lastName = u.LastName,
                photo = u.PhotoURL,
            });
    }
}
